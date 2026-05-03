using Core.DTOs.Common;
using EntityLayer.Entities;

namespace Core.DTOs.RealEstateDtos;

/// <summary>
/// Emlak detay yanıtı
/// </summary>
public class RealEstateDto
{
    public string? RoomCount { get; set; }
    public int? Size { get; set; }
    public int? Floor { get; set; }
    public int? TotalFloors { get; set; }
    public int? BuildingAge { get; set; }
    public bool HasElevator { get; set; }
    public bool HasParking { get; set; }
    public bool IsFurnished { get; set; }

    // Emlak tipi (kategori bilgisini de içerir)
    public HousingTypeLookupDto HousingType { get; set; } = null!;

    // ==================== KONUT — İÇ ====================
    public bool HasBalcony { get; set; }
    public bool HasTerrace { get; set; }
    public bool HasCellar { get; set; }
    public bool HasStorageRoom { get; set; }
    public bool HasFireplace { get; set; }
    public bool HasAirConditioning { get; set; }
    public bool HasUnderfloorHeating { get; set; }
    public bool HasBuiltInKitchen { get; set; }

    // ==================== KONUT — DIŞ ====================
    public bool HasGarden { get; set; }
    public bool HasPool { get; set; }
    public bool HasCoveredParking { get; set; }

    // ==================== KONUT — GÜVENLİK ====================
    public bool HasSecurity { get; set; }
    public bool HasSteelDoor { get; set; }
    public bool HasVideoIntercom { get; set; }
    public bool HasAlarm { get; set; }

    // ==================== KONUT — ALTYAPI ====================
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

    // ==================== ORTAK (İş Yeri + Arsa) ====================
    public bool? IsCreditEligible { get; set; }
    public bool? IsExchangeable { get; set; }

    // ==================== İŞ YERİ ====================
    public int? GrossArea { get; set; }
    public int? NetArea { get; set; }
    public decimal? CeilingHeight { get; set; }
    public decimal? FrontWidth { get; set; }
    public HeatingType? HeatingType { get; set; }
    public BuildingCondition? BuildingCondition { get; set; }
    public UsageStatus? UsageStatus { get; set; }
    public BuildingType? BuildingType { get; set; }
    public bool? HasShowcase { get; set; }
    public bool? HasShutter { get; set; }
    public bool? HasMezzanine { get; set; }
    public bool? HasBasement { get; set; }
    public bool? HasLoadingDock { get; set; }
    public bool? HasColdStorage { get; set; }
    public bool? HasFireSystem { get; set; }
    public bool? HasCameraSystem { get; set; }
    public bool? HasThreePhasePower { get; set; }

    // ==================== ARSA ====================
    public decimal? FloorAreaRatio { get; set; }
    public decimal? HeightLimit { get; set; }
    public string? BlockNumber { get; set; }
    public string? ParcelNumber { get; set; }
    public string? SheetNumber { get; set; }
    public ZoningStatus? ZoningStatus { get; set; }
    public DeedStatus? DeedStatus { get; set; }
    public bool? IsRoadAccessible { get; set; }
    public bool? HasWaterSource { get; set; }
    public bool? HasElectricityConnection { get; set; }
}
