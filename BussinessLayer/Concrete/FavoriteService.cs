using BussinessLayer.Abstract;
using Core.DTOs.Common;
using DataAcessLayer.Concrete;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Concrete;

/// <summary>
/// Kullanıcı favori yönetimi servisi implementasyonu
/// </summary>
public class FavoriteService : IFavoriteService
{
    private readonly Context _context;

    public FavoriteService(Context context)
    {
        _context = context;
    }

    /// <summary>
    /// Kullanıcının favori ilan ID'lerini getir
    /// </summary>
    public async Task<ApiResponseDto<List<Guid>>> GetUserFavoritesAsync(Guid userId)
    {
        var favoriteIds = await _context.UserFavorites
            .Where(f => f.UserId == userId)
            .Select(f => f.ListingId)
            .ToListAsync();

        return ApiResponseDto<List<Guid>>.SuccessResponse(favoriteIds);
    }

    /// <summary>
    /// Favorilere ilan ekle
    /// </summary>
    public async Task<ApiResponseDto> AddFavoriteAsync(Guid userId, Guid listingId)
    {
        // Zaten favori mi kontrol et
        var exists = await _context.UserFavorites
            .AnyAsync(f => f.UserId == userId && f.ListingId == listingId);

        if (exists)
        {
            return ApiResponseDto.FailResponse("Bu ilan zaten favorilerinizde.");
        }

        // Listing var mı kontrol et
        var listingExists = await _context.Listings.AnyAsync(l => l.Id == listingId);
        if (!listingExists)
        {
            return ApiResponseDto.FailResponse("İlan bulunamadı.");
        }

        var favorite = new UserFavorite
        {
            UserId = userId,
            ListingId = listingId,
            AddedAt = DateTime.UtcNow
        };

        _context.UserFavorites.Add(favorite);
        await _context.SaveChangesAsync();

        return ApiResponseDto.SuccessResponse("İlan favorilere eklendi.");
    }

    /// <summary>
    /// Favorilerden ilan çıkar
    /// </summary>
    public async Task<ApiResponseDto> RemoveFavoriteAsync(Guid userId, Guid listingId)
    {
        var favorite = await _context.UserFavorites
            .FirstOrDefaultAsync(f => f.UserId == userId && f.ListingId == listingId);

        if (favorite == null)
        {
            return ApiResponseDto.FailResponse("Bu ilan favorilerinizde değil.");
        }

        _context.UserFavorites.Remove(favorite);
        await _context.SaveChangesAsync();

        return ApiResponseDto.SuccessResponse("İlan favorilerden çıkarıldı.");
    }

    /// <summary>
    /// İlan favori mi kontrol et
    /// </summary>
    public async Task<ApiResponseDto<bool>> IsFavoriteAsync(Guid userId, Guid listingId)
    {
        var isFavorite = await _context.UserFavorites
            .AnyAsync(f => f.UserId == userId && f.ListingId == listingId);

        return ApiResponseDto<bool>.SuccessResponse(isFavorite);
    }
}
