namespace Core.DTOs.ImageDtos;

/// <summary>
/// İlan görseli yanıtı
/// </summary>
public class ImageDto
{
    public Guid Id { get; set; }
    public string Url { get; set; } = string.Empty;
    public int Order { get; set; }
}
