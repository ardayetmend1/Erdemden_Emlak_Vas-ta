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

    // ==================== Araç Filtreleri ====================
    public Guid? VehicleTypeId { get; set; }
    public Guid? BrandId { get; set; }
    public Guid? ModelId { get; set; }
    public Guid? BodyTypeId { get; set; }
    public Guid? PackageId { get; set; }
    public Guid? FuelTypeId { get; set; }
    public Guid? TransmissionTypeId { get; set; }
    public int? MinYear { get; set; }
    public int? MaxYear { get; set; }
    public int? MinKm { get; set; }
    public int? MaxKm { get; set; }

    // ==================== Emlak Filtreleri ====================
    public Guid? HousingTypeId { get; set; }
    public string? RoomCount { get; set; }
    public int? MinSize { get; set; }
    public int? MaxSize { get; set; }

    // ==================== Sıralama ====================
    /// <summary>
    /// Sıralama seçeneği: "price_asc", "price_desc", "newest", "oldest"
    /// </summary>
    public string? SortBy { get; set; }
}
