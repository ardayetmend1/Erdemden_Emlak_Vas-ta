using Core.DTOs.Common;

namespace Core.DTOs.RealEstateDtos;

/// <summary>
/// Emlak detay yanıtı
/// </summary>
public class RealEstateDto
{
    public string RoomCount { get; set; } = string.Empty;
    public int Size { get; set; }
    public int? Floor { get; set; }
    public int? TotalFloors { get; set; }
    public int? BuildingAge { get; set; }
    public bool HasElevator { get; set; }
    public bool HasParking { get; set; }
    public bool IsFurnished { get; set; }

    // Emlak tipi
    public LookupDto HousingType { get; set; } = null!;

    /// <summary>
    /// 0 = Satılık, 1 = Kiralık
    /// </summary>
    public int ListingType { get; set; }

    /// <summary>
    /// Satılık/Kiralık metni
    /// </summary>
    public string ListingTypeText => ListingType == 1 ? "Kiralık" : "Satılık";

    /// <summary>
    /// Aylık kira ücreti (Kiralık için)
    /// </summary>
    public decimal? MonthlyRent { get; set; }

    /// <summary>
    /// Depozito (Kiralık için)
    /// </summary>
    public decimal? Deposit { get; set; }
}
