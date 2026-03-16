using BussinessLayer.Abstract;
using DataAcessLayer.Concrete;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Erdemden.API.Controllers;

/// <summary>
/// Mevcut Base64 görselleri dosya sistemine taşımak için tek seferlik migration endpoint'leri
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "AdminOnly")]
public class MigrationController : ControllerBase
{
    private readonly Context _context;
    private readonly IImageService _imageService;

    public MigrationController(Context context, IImageService imageService)
    {
        _context = context;
        _imageService = imageService;
    }

    /// <summary>
    /// Mevcut Base64 ilan görsellerini dosya sistemine taşır
    /// </summary>
    [HttpPost("migrate-images")]
    public async Task<IActionResult> MigrateImagesToFileSystem()
    {
        var images = await _context.Set<ListingImage>()
            .Where(i => i.Base64Data != null && i.Base64Data != "" && (i.ImageUrl == null || i.ImageUrl == ""))
            .ToListAsync();

        var migrated = 0;
        var failed = 0;

        foreach (var image in images)
        {
            try
            {
                var base64 = image.Base64Data!;
                // MimeType bilgisinden data URI oluştur
                if (!base64.Contains(",") && !string.IsNullOrEmpty(image.MimeType))
                {
                    base64 = $"data:{image.MimeType};base64,{base64}";
                }

                var imageUrl = await _imageService.SaveImageAsync(base64, image.FileName);
                image.ImageUrl = imageUrl;
                image.Base64Data = null; // Base64'ü temizle
                migrated++;
            }
            catch (Exception ex)
            {
                failed++;
                Console.WriteLine($"Migration failed for image {image.Id}: {ex.Message}");
            }
        }

        await _context.SaveChangesAsync();

        return Ok(new { migrated, failed, total = images.Count });
    }

    /// <summary>
    /// Mevcut Base64 site content görsellerini dosya sistemine taşır
    /// </summary>
    [HttpPost("migrate-site-content-images")]
    public async Task<IActionResult> MigrateSiteContentImages()
    {
        var contents = await _context.Set<SiteContent>()
            .Where(c => c.Image != null && c.Image != "")
            .ToListAsync();

        var migrated = 0;
        var skipped = 0;

        foreach (var content in contents)
        {
            // Zaten URL ise atla
            if (content.Image!.StartsWith("/uploads/") || content.Image.StartsWith("http"))
            {
                skipped++;
                continue;
            }

            try
            {
                var imageUrl = await _imageService.SaveImageAsync(content.Image);
                content.Image = imageUrl;
                migrated++;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Migration failed for site content {content.Key}: {ex.Message}");
            }
        }

        await _context.SaveChangesAsync();

        return Ok(new { migrated, skipped, total = contents.Count });
    }
}
