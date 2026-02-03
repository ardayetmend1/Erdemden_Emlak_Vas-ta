namespace Core.DTOs.ImageDtos;

/// <summary>
/// İlan görseli yanıtı
/// </summary>
public class ImageDto
{
    public Guid Id { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsCover { get; set; }
    public int Order { get; set; }
}
