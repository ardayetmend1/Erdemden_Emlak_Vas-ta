namespace Core.DTOs.SiteContentDtos;

/// <summary>
/// Site içeriği yanıtı
/// </summary>
public class SiteContentDto
{
    public Guid Id { get; set; }
    public string Key { get; set; } = string.Empty;
    public string? Image { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
