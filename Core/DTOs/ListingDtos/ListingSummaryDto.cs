using EntityLayer.Entities;

namespace Core.DTOs.ListingDtos;

/// <summary>
/// İlan özet bilgisi (Liste kartları için)
/// </summary>
public class ListingSummaryDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Currency { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public ListingCategory Category { get; set; }
    public ListingStatus Status { get; set; }
    public string? Location { get; set; }
    public DateTime ListingDate { get; set; }

    // Araç özet bilgileri
    public int? Year { get; set; }
    public int? Km { get; set; }
    public string? BrandName { get; set; }
    public string? ModelName { get; set; }

    // Emlak özet bilgileri
    public string? RoomCount { get; set; }
    public int? Size { get; set; }
    public string? HousingTypeName { get; set; }
}
