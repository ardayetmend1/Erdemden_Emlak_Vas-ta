using BussinessLayer.Abstract;
using Core.DTOs.Common;
using Core.DTOs.SiteContentDtos;
using DataAcessLayer.Concrete;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Concrete;

/// <summary>
/// Site içerik yönetimi servisi implementasyonu
/// </summary>
public class SiteContentService : ISiteContentService
{
    private readonly Context _context;
    private readonly IImageService _imageService;

    public SiteContentService(Context context, IImageService imageService)
    {
        _context = context;
        _imageService = imageService;
    }

    /// <summary>
    /// Tüm site içeriklerini getir
    /// </summary>
    public async Task<ApiResponseDto<Dictionary<string, SiteContentDto>>> GetAllContentAsync()
    {
        var contents = await _context.SiteContents.ToListAsync();

        var result = contents.ToDictionary(
            c => c.Key,
            c => MapToDto(c)
        );

        return ApiResponseDto<Dictionary<string, SiteContentDto>>.SuccessResponse(result);
    }

    /// <summary>
    /// Belirli bir key'e ait içeriği getir
    /// </summary>
    public async Task<ApiResponseDto<SiteContentDto>> GetContentByKeyAsync(string key)
    {
        var content = await _context.SiteContents
            .FirstOrDefaultAsync(c => c.Key == key);

        if (content == null)
        {
            return ApiResponseDto<SiteContentDto>.FailResponse("İçerik bulunamadı.");
        }

        return ApiResponseDto<SiteContentDto>.SuccessResponse(MapToDto(content));
    }

    /// <summary>
    /// İçerik oluştur veya güncelle (upsert)
    /// </summary>
    public async Task<ApiResponseDto<SiteContentDto>> UpsertContentAsync(string key, UpdateSiteContentDto dto)
    {
        var content = await _context.SiteContents
            .FirstOrDefaultAsync(c => c.Key == key);

        // Image alanı Base64 ise dosyaya kaydet
        var imageValue = dto.Image;
        if (!string.IsNullOrEmpty(imageValue) && IsBase64Image(imageValue))
        {
            imageValue = await _imageService.SaveImageAsync(imageValue);
        }

        if (content == null)
        {
            // Yeni kayıt oluştur
            content = new SiteContent
            {
                Key = key,
                Image = imageValue,
                Title = dto.Title,
                Description = dto.Description
            };

            _context.SiteContents.Add(content);
        }
        else
        {
            // Eski görseli sil (dosya sistemindeyse)
            if (imageValue != null && !string.IsNullOrEmpty(content.Image)
                && content.Image.StartsWith("/uploads/"))
            {
                _imageService.DeleteImage(content.Image);
            }

            // Mevcut kaydı güncelle
            if (imageValue != null) content.Image = imageValue;
            if (dto.Title != null) content.Title = dto.Title;
            if (dto.Description != null) content.Description = dto.Description;
            content.UpdatedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();

        return ApiResponseDto<SiteContentDto>.SuccessResponse(MapToDto(content), "İçerik kaydedildi.");
    }

    /// <summary>
    /// Base64 data URI mi kontrol et
    /// </summary>
    private static bool IsBase64Image(string value)
    {
        return value.StartsWith("data:image/") || (value.Length > 500 && !value.StartsWith("/") && !value.StartsWith("http"));
    }

    /// <summary>
    /// İçeriği sil
    /// </summary>
    public async Task<ApiResponseDto> DeleteContentAsync(string key)
    {
        var content = await _context.SiteContents
            .FirstOrDefaultAsync(c => c.Key == key);

        if (content == null)
        {
            return ApiResponseDto.FailResponse("İçerik bulunamadı.");
        }

        _context.SiteContents.Remove(content);
        await _context.SaveChangesAsync();

        return ApiResponseDto.SuccessResponse("İçerik silindi.");
    }

    /// <summary>
    /// Entity'yi DTO'ya dönüştür
    /// </summary>
    private static SiteContentDto MapToDto(SiteContent content)
    {
        return new SiteContentDto
        {
            Id = content.Id,
            Key = content.Key,
            Image = content.Image,
            Title = content.Title,
            Description = content.Description,
            CreatedAt = content.CreatedAt,
            UpdatedAt = content.UpdatedAt
        };
    }
}
