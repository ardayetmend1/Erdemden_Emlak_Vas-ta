using Core.DTOs.Common;
using Core.DTOs.QuoteRequestDtos;

namespace BussinessLayer.Abstract;

/// <summary>
/// Teklif talebi yönetimi servisi
/// </summary>
public interface IQuoteService
{
    /// <summary>
    /// Yeni teklif talebi oluştur
    /// </summary>
    Task<ApiResponseDto<QuoteRequestDto>> CreateQuoteAsync(CreateQuoteRequestDto dto, List<FileUploadDto>? expertReports = null);

    /// <summary>
    /// Tüm teklif taleplerini getir (Admin için)
    /// </summary>
    Task<ApiResponseDto<List<QuoteRequestDto>>> GetAllQuotesAsync();

    /// <summary>
    /// Kullanıcının teklif taleplerini getir (Email ile)
    /// </summary>
    Task<ApiResponseDto<List<QuoteRequestDto>>> GetQuotesByEmailAsync(string email);

    /// <summary>
    /// Teklif talebini ID ile getir
    /// </summary>
    Task<ApiResponseDto<QuoteRequestDto>> GetQuoteByIdAsync(Guid id);

    /// <summary>
    /// Teklif talebini okundu olarak işaretle
    /// </summary>
    Task<ApiResponseDto> MarkAsReadAsync(Guid id);

    /// <summary>
    /// Teklif talebini sil
    /// </summary>
    Task<ApiResponseDto> DeleteQuoteAsync(Guid id);
}

/// <summary>
/// Dosya yükleme DTO
/// </summary>
public class FileUploadDto
{
    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;
    public string Base64Data { get; set; } = string.Empty;
}
