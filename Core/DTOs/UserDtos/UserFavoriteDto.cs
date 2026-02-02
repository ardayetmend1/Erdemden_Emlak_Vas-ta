namespace Core.DTOs.UserDtos;

/// <summary>
/// Kullanıcı favorileri yanıtı
/// </summary>
public class UserFavoriteDto
{
    public Guid ListingId { get; set; }
    public string ListingTitle { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public string Category { get; set; } = string.Empty;
    public DateTime AddedAt { get; set; }
}
