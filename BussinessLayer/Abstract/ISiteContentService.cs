using Core.DTOs.Common;
using Core.DTOs.SiteContentDtos;

namespace BussinessLayer.Abstract;

/// <summary>
/// Site içerik yönetimi servisi
/// </summary>
public interface ISiteContentService
{
    /// <summary>
    /// Tüm site içeriklerini getir (key -> content)
    /// </summary>
    Task<ApiResponseDto<Dictionary<string, SiteContentDto>>> GetAllContentAsync();

    /// <summary>
    /// Belirli bir key'e ait içeriği getir
    /// </summary>
    Task<ApiResponseDto<SiteContentDto>> GetContentByKeyAsync(string key);

    /// <summary>
    /// İçerik oluştur veya güncelle (upsert)
    /// </summary>
    Task<ApiResponseDto<SiteContentDto>> UpsertContentAsync(string key, UpdateSiteContentDto dto);

    /// <summary>
    /// İçeriği sil
    /// </summary>
    Task<ApiResponseDto> DeleteContentAsync(string key);
}
