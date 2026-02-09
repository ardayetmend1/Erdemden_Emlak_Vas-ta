using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Entities;

public class RealEstate : BaseEntity
{
    [ForeignKey("Listing")]
    public Guid ListingId { get; set; }

    public Guid HousingTypeId { get; set; }

    [MaxLength(20)]
    public string? RoomCount { get; set; }

    public int? Size { get; set; }

    public int? Floor { get; set; }

    public int? TotalFloors { get; set; }

    public int? BuildingAge { get; set; }

    public bool? HasElevator { get; set; }

    public bool? HasParking { get; set; }

    public bool? IsFurnished { get; set; }

    // ==================== İÇ ÖZELLİKLER ====================
    public bool? HasBalcony { get; set; }
    public bool? HasTerrace { get; set; }
    public bool? HasCellar { get; set; }
    public bool? HasStorageRoom { get; set; }
    public bool? HasFireplace { get; set; }
    public bool? HasAirConditioning { get; set; }
    public bool? HasUnderfloorHeating { get; set; }
    public bool? HasBuiltInKitchen { get; set; }

    // ==================== DIŞ ÖZELLİKLER ====================
    public bool? HasGarden { get; set; }
    public bool? HasPool { get; set; }
    public bool? HasCoveredParking { get; set; }

    // ==================== GÜVENLİK ====================
    public bool? HasSecurity { get; set; }
    public bool? HasSteelDoor { get; set; }
    public bool? HasVideoIntercom { get; set; }
    public bool? HasAlarm { get; set; }

    // ==================== ALTYAPI ====================
    public bool? HasSatellite { get; set; }
    public bool? HasCableTv { get; set; }
    public bool? HasInternet { get; set; }
    public bool? HasGenerator { get; set; }
    public bool? HasNaturalGas { get; set; }

    // Kiralık/Satılık bilgileri
    public RealEstateListingType ListingType { get; set; } = RealEstateListingType.Satilik;

    public decimal? MonthlyRent { get; set; }

    public decimal? Deposit { get; set; }

    // Navigation
    public virtual Listing Listing { get; set; } = null!;
    public virtual HousingType HousingType { get; set; } = null!;
}
