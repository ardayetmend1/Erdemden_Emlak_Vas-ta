using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.SiteContentDtos;

/// <summary>
/// Site içeriği oluşturma isteği
/// </summary>
public class CreateSiteContentDto
{
    [Required(ErrorMessage = "Anahtar gereklidir")]
    [StringLength(100)]
    public string Key { get; set; } = string.Empty;

    public string? Image { get; set; }

    [StringLength(200)]
    public string? Title { get; set; }

    public string? Description { get; set; }
}
