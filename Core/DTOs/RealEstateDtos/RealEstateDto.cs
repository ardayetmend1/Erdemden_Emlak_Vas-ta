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

    // ==================== İÇ ÖZELLİKLER ====================
    public bool HasBalcony { get; set; }
    public bool HasTerrace { get; set; }
    public bool HasCellar { get; set; }
    public bool HasStorageRoom { get; set; }
    public bool HasFireplace { get; set; }
    public bool HasAirConditioning { get; set; }
    public bool HasUnderfloorHeating { get; set; }
    public bool HasBuiltInKitchen { get; set; }

    // ==================== DIŞ ÖZELLİKLER ====================
    public bool HasGarden { get; set; }
    public bool HasPool { get; set; }
    public bool HasCoveredParking { get; set; }

    // ==================== GÜVENLİK ====================
    public bool HasSecurity { get; set; }
    public bool HasSteelDoor { get; set; }
    public bool HasVideoIntercom { get; set; }
    public bool HasAlarm { get; set; }

    // ==================== ALTYAPI ====================
    public bool HasSatellite { get; set; }
    public bool HasCableTv { get; set; }
    public bool HasInternet { get; set; }
    public bool HasGenerator { get; set; }
    public bool HasNaturalGas { get; set; }

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
