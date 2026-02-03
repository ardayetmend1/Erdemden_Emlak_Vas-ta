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

    public SiteContentService(Context context)
    {
        _context = context;
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

        if (content == null)
        {
            // Yeni kayıt oluştur
            content = new SiteContent
            {
                Key = key,
                Image = dto.Image,
                Title = dto.Title,
                Description = dto.Description
            };

            _context.SiteContents.Add(content);
        }
        else
        {
            // Mevcut kaydı güncelle
            if (dto.Image != null) content.Image = dto.Image;
            if (dto.Title != null) content.Title = dto.Title;
            if (dto.Description != null) content.Description = dto.Description;
            content.UpdatedAt = DateTime.UtcNow;
        }

        await _context.SaveChangesAsync();

        return ApiResponseDto<SiteContentDto>.SuccessResponse(MapToDto(content), "İçerik kaydedildi.");
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
