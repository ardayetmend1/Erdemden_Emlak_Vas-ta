using Core.DTOs.Common;

namespace BussinessLayer.Abstract;

/// <summary>
/// Kullanıcı favori yönetimi servisi
/// </summary>
public interface IFavoriteService
{
    /// <summary>
    /// Kullanıcının favori ilan ID'lerini getir
    /// </summary>
    Task<ApiResponseDto<List<Guid>>> GetUserFavoritesAsync(Guid userId);

    /// <summary>
    /// Favorilere ilan ekle
    /// </summary>
    Task<ApiResponseDto> AddFavoriteAsync(Guid userId, Guid listingId);

    /// <summary>
    /// Favorilerden ilan çıkar
    /// </summary>
    Task<ApiResponseDto> RemoveFavoriteAsync(Guid userId, Guid listingId);

    /// <summary>
    /// İlan favori mi kontrol et
    /// </summary>
    Task<ApiResponseDto<bool>> IsFavoriteAsync(Guid userId, Guid listingId);
}
