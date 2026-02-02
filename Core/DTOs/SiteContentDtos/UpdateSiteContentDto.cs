using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.SiteContentDtos;

/// <summary>
/// Site içeriği güncelleme isteği
/// </summary>
public class UpdateSiteContentDto
{
    public string? Image { get; set; }

    [StringLength(200)]
    public string? Title { get; set; }

    public string? Description { get; set; }
}
