using System.Net;
using BussinessLayer.Abstract;
using BussinessLayer.Settings;
using Core.DTOs.AnalyticsDtos;
using Core.DTOs.Common;
using Google;
using Google.Apis.AnalyticsData.v1beta;
using Google.Apis.AnalyticsData.v1beta.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BussinessLayer.Concrete;

public class AnalyticsService : IAnalyticsService
{
    private const string AnalyticsScope = "https://www.googleapis.com/auth/analytics.readonly";

    private readonly GoogleAnalyticsSettings _settings;
    private readonly IMemoryCache _cache;
    private readonly ILogger<AnalyticsService> _logger;

    public AnalyticsService(
        IOptions<GoogleAnalyticsSettings> settings,
        IMemoryCache cache,
        ILogger<AnalyticsService> logger)
    {
        _settings = settings.Value;
        _cache = cache;
        _logger = logger;
    }

    public async Task<ApiResponseDto<AnalyticsOverviewDto>> GetOverviewAsync(int days)
    {
        var cacheKey = $"analytics_overview_{days}";
        if (_cache.TryGetValue(cacheKey, out AnalyticsOverviewDto? cached) && cached != null)
        {
            return ApiResponseDto<AnalyticsOverviewDto>.SuccessResponse(cached);
        }

        var propertyId = Environment.GetEnvironmentVariable("GoogleAnalyticsSettings__PropertyId")
            ?? _settings.PropertyId;
        var credentialsFilePath = Environment.GetEnvironmentVariable("GoogleAnalyticsSettings__CredentialsFilePath")
            ?? _settings.CredentialsFilePath;

        var validationError = ValidateConfiguration(propertyId, credentialsFilePath);
        if (validationError != null)
        {
            return validationError;
        }

        try
        {
            var credential = GoogleCredential
                .FromFile(credentialsFilePath)
                .CreateScoped(AnalyticsScope);

            var service = new AnalyticsDataService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = "ErdemdenAnalytics"
            });

            var propertyResourceName = propertyId.StartsWith("properties/", StringComparison.OrdinalIgnoreCase)
                ? propertyId
                : $"properties/{propertyId}";

            var startDate = $"{days}daysAgo";
            var endDate = "today";

            var summaryTask = RunSummaryMetricsReport(service, propertyResourceName, startDate, endDate);
            var dailyTask = RunDailyMetricsReport(service, propertyResourceName, startDate, endDate);
            var trafficTask = RunTrafficSourcesReport(service, propertyResourceName, startDate, endDate);
            var pagesTask = RunPopularPagesReport(service, propertyResourceName, startDate, endDate);
            var devicesTask = RunDeviceBreakdownReport(service, propertyResourceName, startDate, endDate);

            await Task.WhenAll(summaryTask, dailyTask, trafficTask, pagesTask, devicesTask);

            var overview = new AnalyticsOverviewDto
            {
                ActiveUsers = summaryTask.Result.ActiveUsers,
                NewUsers = summaryTask.Result.NewUsers,
                AverageEngagementTimePerActiveUserSeconds = summaryTask.Result.AverageEngagementTimePerActiveUserSeconds,
                EventCount = summaryTask.Result.EventCount,
                DailyMetrics = dailyTask.Result
                    .Select(d => new DailyMetricDto
                    {
                        Date = d.Date,
                        ActiveUsers = d.ActiveUsers,
                        NewUsers = d.NewUsers,
                        EventCount = d.EventCount
                    })
                    .OrderBy(d => d.Date)
                    .ToList(),
                TrafficSources = trafficTask.Result,
                PopularPages = pagesTask.Result,
                DeviceBreakdown = devicesTask.Result
            };

            _cache.Set(cacheKey, overview, TimeSpan.FromMinutes(5));

            return ApiResponseDto<AnalyticsOverviewDto>.SuccessResponse(overview);
        }
        catch (GoogleApiException ex) when (ex.HttpStatusCode == HttpStatusCode.Forbidden)
        {
            _logger.LogWarning(ex, "Google Analytics access forbidden for property {PropertyId}.", propertyId);
            return ApiResponseDto<AnalyticsOverviewDto>.FailResponse(
                "Google Analytics yetkisi bulunamadı. Service account e-postasına property üzerinde en az Viewer yetkisi verildiğini kontrol edin.",
                "analytics_permission_denied");
        }
        catch (GoogleApiException ex) when (ex.HttpStatusCode == HttpStatusCode.Unauthorized)
        {
            _logger.LogWarning(ex, "Google Analytics authentication failed for property {PropertyId}.", propertyId);
            return ApiResponseDto<AnalyticsOverviewDto>.FailResponse(
                "Google Analytics kimlik doğrulaması başarısız oldu. Service account JSON dosyasını kontrol edin.",
                "analytics_auth_failed");
        }
        catch (FileNotFoundException ex)
        {
            _logger.LogWarning(ex, "Google Analytics credentials file not found.");
            return ApiResponseDto<AnalyticsOverviewDto>.FailResponse(
                "Google Analytics servis hesabı dosyası bulunamadı. Sunucudaki credentials dosya yolunu kontrol edin.",
                "analytics_credentials_missing");
        }
        catch (DirectoryNotFoundException ex)
        {
            _logger.LogWarning(ex, "Google Analytics credentials directory not found.");
            return ApiResponseDto<AnalyticsOverviewDto>.FailResponse(
                "Google Analytics credentials klasörü bulunamadı. Sunucu yolunu kontrol edin.",
                "analytics_credentials_directory_missing");
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Google Analytics credentials file could not be accessed.");
            return ApiResponseDto<AnalyticsOverviewDto>.FailResponse(
                "Google Analytics credentials dosyasına erişilemiyor. Dosya izinlerini kontrol edin.",
                "analytics_credentials_access_denied");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while retrieving Google Analytics overview.");
            return ApiResponseDto<AnalyticsOverviewDto>.FailResponse(
                "Google Analytics verileri şu anda alınamadı. Property ayarlarını ve service account bağlantısını kontrol edin.",
                "analytics_unavailable");
        }
    }

    private ApiResponseDto<AnalyticsOverviewDto>? ValidateConfiguration(string propertyId, string credentialsFilePath)
    {
        if (string.IsNullOrWhiteSpace(propertyId))
        {
            return ApiResponseDto<AnalyticsOverviewDto>.FailResponse(
                "Google Analytics Property ID yapılandırılmamış. GoogleAnalyticsSettings__PropertyId değerini ekleyin.",
                "analytics_property_missing");
        }

        if (string.IsNullOrWhiteSpace(credentialsFilePath))
        {
            return ApiResponseDto<AnalyticsOverviewDto>.FailResponse(
                "Google Analytics credentials dosya yolu yapılandırılmamış. GoogleAnalyticsSettings__CredentialsFilePath değerini ekleyin.",
                "analytics_credentials_path_missing");
        }

        if (!File.Exists(credentialsFilePath))
        {
            return ApiResponseDto<AnalyticsOverviewDto>.FailResponse(
                "Google Analytics servis hesabı dosyası bulunamadı. Sunucudaki credentials dosya yolunu kontrol edin.",
                "analytics_credentials_missing");
        }

        return null;
    }

    private async Task<SummaryMetricsRaw> RunSummaryMetricsReport(
        AnalyticsDataService service, string propertyId, string startDate, string endDate)
    {
        var request = new RunReportRequest
        {
            DateRanges = new[] { new DateRange { StartDate = startDate, EndDate = endDate } },
            Metrics = new[]
            {
                new Metric { Name = "activeUsers" },
                new Metric { Name = "newUsers" },
                new Metric { Name = "eventCount" },
                new Metric
                {
                    Name = "averageEngagementTimePerActiveUser",
                    Expression = "userEngagementDuration/activeUsers"
                }
            }
        };

        var response = await service.Properties.RunReport(request, propertyId).ExecuteAsync();
        var row = response.Rows?.FirstOrDefault();

        if (row == null)
        {
            return new SummaryMetricsRaw();
        }

        return new SummaryMetricsRaw
        {
            ActiveUsers = GetIntValue(row, 0),
            NewUsers = GetIntValue(row, 1),
            EventCount = GetIntValue(row, 2),
            AverageEngagementTimePerActiveUserSeconds = GetDoubleValue(row, 3)
        };
    }

    private async Task<List<DailyMetricRaw>> RunDailyMetricsReport(
        AnalyticsDataService service, string propertyId, string startDate, string endDate)
    {
        var request = new RunReportRequest
        {
            DateRanges = new[] { new DateRange { StartDate = startDate, EndDate = endDate } },
            Dimensions = new[] { new Dimension { Name = "date" } },
            Metrics = new[]
            {
                new Metric { Name = "activeUsers" },
                new Metric { Name = "newUsers" },
                new Metric { Name = "eventCount" }
            }
        };

        var response = await service.Properties.RunReport(request, propertyId).ExecuteAsync();

        var results = new List<DailyMetricRaw>();
        if (response.Rows == null)
        {
            return results;
        }

        foreach (var row in response.Rows)
        {
            var dateStr = row.DimensionValues[0].Value;
            var formatted = $"{dateStr[..4]}-{dateStr[4..6]}-{dateStr[6..8]}";

            results.Add(new DailyMetricRaw
            {
                Date = formatted,
                ActiveUsers = GetIntValue(row, 0),
                NewUsers = GetIntValue(row, 1),
                EventCount = GetIntValue(row, 2)
            });
        }

        return results;
    }

    private async Task<List<TrafficSourceDto>> RunTrafficSourcesReport(
        AnalyticsDataService service, string propertyId, string startDate, string endDate)
    {
        var request = new RunReportRequest
        {
            DateRanges = new[] { new DateRange { StartDate = startDate, EndDate = endDate } },
            Dimensions = new[] { new Dimension { Name = "sessionSource" } },
            Metrics = new[] { new Metric { Name = "sessions" } },
            OrderBys = new[] { new OrderBy { Metric = new MetricOrderBy { MetricName = "sessions" }, Desc = true } },
            Limit = 10
        };

        var response = await service.Properties.RunReport(request, propertyId).ExecuteAsync();

        var results = new List<TrafficSourceDto>();
        if (response.Rows == null)
        {
            return results;
        }

        var total = response.Rows.Sum(r => GetIntValue(r, 0));

        foreach (var row in response.Rows)
        {
            var count = GetIntValue(row, 0);
            results.Add(new TrafficSourceDto
            {
                Source = row.DimensionValues[0].Value ?? "(direct)",
                Count = count,
                Percentage = total > 0 ? Math.Round((double)count / total * 100, 1) : 0
            });
        }

        return results;
    }

    private async Task<List<PopularPageDto>> RunPopularPagesReport(
        AnalyticsDataService service, string propertyId, string startDate, string endDate)
    {
        var request = new RunReportRequest
        {
            DateRanges = new[] { new DateRange { StartDate = startDate, EndDate = endDate } },
            Dimensions = new[]
            {
                new Dimension { Name = "pagePathPlusQueryString" },
                new Dimension { Name = "pageTitle" }
            },
            Metrics = new[] { new Metric { Name = "screenPageViews" } },
            OrderBys = new[] { new OrderBy { Metric = new MetricOrderBy { MetricName = "screenPageViews" }, Desc = true } },
            Limit = 10
        };

        var response = await service.Properties.RunReport(request, propertyId).ExecuteAsync();

        var results = new List<PopularPageDto>();
        if (response.Rows == null)
        {
            return results;
        }

        foreach (var row in response.Rows)
        {
            results.Add(new PopularPageDto
            {
                PagePath = row.DimensionValues[0].Value,
                PageTitle = row.DimensionValues[1].Value,
                Views = GetIntValue(row, 0)
            });
        }

        return results;
    }

    private async Task<List<DeviceBreakdownDto>> RunDeviceBreakdownReport(
        AnalyticsDataService service, string propertyId, string startDate, string endDate)
    {
        var request = new RunReportRequest
        {
            DateRanges = new[] { new DateRange { StartDate = startDate, EndDate = endDate } },
            Dimensions = new[] { new Dimension { Name = "deviceCategory" } },
            Metrics = new[] { new Metric { Name = "activeUsers" } },
            OrderBys = new[] { new OrderBy { Metric = new MetricOrderBy { MetricName = "activeUsers" }, Desc = true } }
        };

        var response = await service.Properties.RunReport(request, propertyId).ExecuteAsync();

        var results = new List<DeviceBreakdownDto>();
        if (response.Rows == null)
        {
            return results;
        }

        var total = response.Rows.Sum(r => GetIntValue(r, 0));

        foreach (var row in response.Rows)
        {
            var count = GetIntValue(row, 0);
            results.Add(new DeviceBreakdownDto
            {
                DeviceCategory = row.DimensionValues[0].Value,
                Count = count,
                Percentage = total > 0 ? Math.Round((double)count / total * 100, 1) : 0
            });
        }

        return results;
    }

    private static int GetIntValue(Row row, int metricIndex)
    {
        return int.TryParse(row.MetricValues[metricIndex].Value, out var value) ? value : 0;
    }

    private static double GetDoubleValue(Row row, int metricIndex)
    {
        return double.TryParse(row.MetricValues[metricIndex].Value, out var value) ? value : 0;
    }

    private class SummaryMetricsRaw
    {
        public int ActiveUsers { get; set; }
        public int NewUsers { get; set; }
        public int EventCount { get; set; }
        public double AverageEngagementTimePerActiveUserSeconds { get; set; }
    }

    private class DailyMetricRaw
    {
        public string Date { get; set; } = string.Empty;
        public int ActiveUsers { get; set; }
        public int NewUsers { get; set; }
        public int EventCount { get; set; }
    }
}
