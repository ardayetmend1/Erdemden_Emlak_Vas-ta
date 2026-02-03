using BussinessLayer.Abstract;
using Core.DTOs.Common;
using Core.DTOs.DocumentDtos;
using Core.DTOs.QuoteRequestDtos;
using DataAcessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Concrete;

/// <summary>
/// Teklif talebi yönetimi servisi implementasyonu
/// </summary>
public class QuoteService : IQuoteService
{
    private readonly IUnitOfWork _unitOfWork;

    public QuoteService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Yeni teklif talebi oluştur
    /// </summary>
    public async Task<ApiResponseDto<QuoteRequestDto>> CreateQuoteAsync(CreateQuoteRequestDto dto, List<FileUploadDto>? expertReports = null)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            // QuoteRequest oluştur
            var quoteRequest = new QuoteRequest
            {
                Date = DateTime.UtcNow,
                Plate = dto.Plate,
                Brand = dto.Brand,
                Model = dto.Model,
                Year = dto.Year,
                Km = dto.Km,
                Gear = dto.Gear,
                Fuel = dto.Fuel,
                Damage = dto.Damage,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Phone = dto.Phone,
                Email = dto.Email,
                IsRead = false
            };

            await _unitOfWork.Repository<QuoteRequest>().AddAsync(quoteRequest);
            await _unitOfWork.SaveChangesAsync();

            // Ekspertiz raporlarını ekle
            if (expertReports != null && expertReports.Count > 0)
            {
                foreach (var report in expertReports)
                {
                    var expertReport = new ExpertReport
                    {
                        QuoteRequestId = quoteRequest.Id,
                        Name = report.FileName,
                        ContentType = report.ContentType,
                        Data = Convert.FromBase64String(report.Base64Data)
                    };

                    await _unitOfWork.Repository<ExpertReport>().AddAsync(expertReport);
                }

                await _unitOfWork.SaveChangesAsync();
            }

            await _unitOfWork.CommitTransactionAsync();

            // Response DTO oluştur
            var responseDto = await MapToDto(quoteRequest);
            return ApiResponseDto<QuoteRequestDto>.SuccessResponse(responseDto, "Teklif talebiniz başarıyla alındı.");
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return ApiResponseDto<QuoteRequestDto>.FailResponse($"Teklif oluşturulurken hata: {ex.Message}");
        }
    }

    /// <summary>
    /// Tüm teklif taleplerini getir (Admin için)
    /// </summary>
    public async Task<ApiResponseDto<List<QuoteRequestDto>>> GetAllQuotesAsync()
    {
        var quotes = await _unitOfWork.Repository<QuoteRequest>()
            .Query()
            .Include(q => q.ExpertReports)
            .OrderByDescending(q => q.Date)
            .ToListAsync();

        var dtos = quotes.Select(q => MapToDto(q).Result).ToList();
        return ApiResponseDto<List<QuoteRequestDto>>.SuccessResponse(dtos);
    }

    /// <summary>
    /// Kullanıcının teklif taleplerini getir (Email ile)
    /// </summary>
    public async Task<ApiResponseDto<List<QuoteRequestDto>>> GetQuotesByEmailAsync(string email)
    {
        var quotes = await _unitOfWork.Repository<QuoteRequest>()
            .Query()
            .Include(q => q.ExpertReports)
            .Where(q => q.Email != null && q.Email.ToLower() == email.ToLower())
            .OrderByDescending(q => q.Date)
            .ToListAsync();

        var dtos = quotes.Select(q => MapToDto(q).Result).ToList();
        return ApiResponseDto<List<QuoteRequestDto>>.SuccessResponse(dtos);
    }

    /// <summary>
    /// Teklif talebini ID ile getir
    /// </summary>
    public async Task<ApiResponseDto<QuoteRequestDto>> GetQuoteByIdAsync(Guid id)
    {
        var quote = await _unitOfWork.Repository<QuoteRequest>()
            .Query()
            .Include(q => q.ExpertReports)
            .FirstOrDefaultAsync(q => q.Id == id);

        if (quote == null)
            return ApiResponseDto<QuoteRequestDto>.FailResponse("Teklif talebi bulunamadı.");

        var dto = await MapToDto(quote);
        return ApiResponseDto<QuoteRequestDto>.SuccessResponse(dto);
    }

    /// <summary>
    /// Teklif talebini okundu olarak işaretle
    /// </summary>
    public async Task<ApiResponseDto> MarkAsReadAsync(Guid id)
    {
        var quote = await _unitOfWork.Repository<QuoteRequest>().GetByIdAsync(id);

        if (quote == null)
            return ApiResponseDto.FailResponse("Teklif talebi bulunamadı.");

        quote.IsRead = true;
        _unitOfWork.Repository<QuoteRequest>().Update(quote);
        await _unitOfWork.SaveChangesAsync();

        return ApiResponseDto.SuccessResponse("Teklif okundu olarak işaretlendi.");
    }

    /// <summary>
    /// Teklif talebini sil
    /// </summary>
    public async Task<ApiResponseDto> DeleteQuoteAsync(Guid id)
    {
        var quote = await _unitOfWork.Repository<QuoteRequest>()
            .Query()
            .Include(q => q.ExpertReports)
            .FirstOrDefaultAsync(q => q.Id == id);

        if (quote == null)
            return ApiResponseDto.FailResponse("Teklif talebi bulunamadı.");

        // Ekspertiz raporlarını sil (Cascade ile otomatik silinir ama explicit olsun)
        foreach (var report in quote.ExpertReports.ToList())
        {
            _unitOfWork.Repository<ExpertReport>().Delete(report);
        }

        _unitOfWork.Repository<QuoteRequest>().Delete(quote);
        await _unitOfWork.SaveChangesAsync();

        return ApiResponseDto.SuccessResponse("Teklif talebi silindi.");
    }

    /// <summary>
    /// Entity'yi DTO'ya map et
    /// </summary>
    private Task<QuoteRequestDto> MapToDto(QuoteRequest quote)
    {
        var dto = new QuoteRequestDto
        {
            Id = quote.Id,
            Date = quote.Date,
            IsRead = quote.IsRead,
            Plate = quote.Plate ?? string.Empty,
            Brand = quote.Brand ?? string.Empty,
            Model = quote.Model ?? string.Empty,
            Year = quote.Year,
            Km = quote.Km,
            Gear = quote.Gear,
            Fuel = quote.Fuel,
            Damage = quote.Damage,
            FirstName = quote.FirstName,
            LastName = quote.LastName,
            Phone = quote.Phone,
            Email = quote.Email,
            ExpertReports = quote.ExpertReports.Select(r => new DocumentDto
            {
                Id = r.Id,
                Name = r.Name,
                ContentType = r.ContentType ?? "application/octet-stream",
                DownloadUrl = $"/api/quotes/reports/{r.Id}/download"
            }).ToList()
        };

        return Task.FromResult(dto);
    }
}
