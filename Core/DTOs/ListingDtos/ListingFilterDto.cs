using Core.DTOs.Common;
using EntityLayer.Entities;

namespace Core.DTOs.ListingDtos;

/// <summary>
/// İlan filtreleme parametreleri
/// </summary>
public class ListingFilterDto : PaginationRequestDto
{
    public ListingCategory? Category { get; set; }
    public ListingStatus? Status { get; set; }

    // Fiyat aralığı
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }

    // Konum
    public Guid? CityId { get; set; }
    public Guid? DistrictId { get; set; }

    // Arama
    public string? SearchTerm { get; set; }

    // Tarih aralığı
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}
