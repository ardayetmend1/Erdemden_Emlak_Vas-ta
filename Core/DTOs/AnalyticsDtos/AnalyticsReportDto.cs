namespace Core.DTOs.AnalyticsDtos;

public class AnalyticsOverviewDto
{
    public int ActiveUsers { get; set; }
    public int NewUsers { get; set; }
    public double AverageEngagementTimePerActiveUserSeconds { get; set; }
    public int EventCount { get; set; }
    public List<DailyMetricDto> DailyMetrics { get; set; } = new();
    public List<TrafficSourceDto> TrafficSources { get; set; } = new();
    public List<PopularPageDto> PopularPages { get; set; } = new();
    public List<DeviceBreakdownDto> DeviceBreakdown { get; set; } = new();
}

public class DailyMetricDto
{
    public string Date { get; set; } = string.Empty;
    public int ActiveUsers { get; set; }
    public int NewUsers { get; set; }
    public int EventCount { get; set; }
}

public class TrafficSourceDto
{
    public string Source { get; set; } = string.Empty;
    public int Count { get; set; }
    public double Percentage { get; set; }
}

public class PopularPageDto
{
    public string PagePath { get; set; } = string.Empty;
    public string PageTitle { get; set; } = string.Empty;
    public int Views { get; set; }
}

public class DeviceBreakdownDto
{
    public string DeviceCategory { get; set; } = string.Empty;
    public int Count { get; set; }
    public double Percentage { get; set; }
}
