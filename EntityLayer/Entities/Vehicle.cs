using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Entities;

public class Vehicle : BaseEntity
{
    [ForeignKey("Listing")]
    public Guid ListingId { get; set; }

    public Guid VehicleTypeId { get; set; }
    public Guid? BodyTypeId { get; set; }
    public Guid BrandId { get; set; }
    public Guid ModelId { get; set; }
    public Guid? PackageId { get; set; }
    public Guid FuelTypeId { get; set; }
    public Guid TransmissionTypeId { get; set; }

    public int? Year { get; set; }
    public int? Km { get; set; }

    [MaxLength(50)]
    public string? Color { get; set; }

    [MaxLength(255)]
    public string? DamageStatus { get; set; }

    // ==================== GÜVENLİK ====================
    public bool HasABS { get; set; }
    public bool HasESP { get; set; }
    public bool HasAirbag { get; set; }
    public bool HasRearCamera { get; set; }
    public bool HasParkingSensor { get; set; }
    public bool HasLaneAssist { get; set; }
    public bool HasBlindSpotWarning { get; set; }
    public bool HasCentralLock { get; set; }
    public bool HasImmobilizer { get; set; }
    public bool HasIsofix { get; set; }

    // ==================== KONFOR ====================
    public bool HasAirConditioning { get; set; }
    public bool HasDigitalAC { get; set; }
    public bool HasLeatherSeats { get; set; }
    public bool HasSeatHeating { get; set; }
    public bool HasElectricWindows { get; set; }
    public bool HasElectricMirrors { get; set; }
    public bool HasSunroof { get; set; }
    public bool HasCruiseControl { get; set; }
    public bool HasSteeringWheelHeating { get; set; }
    public bool HasStartStop { get; set; }

    // ==================== MULTİMEDYA ====================
    public bool HasBluetooth { get; set; }
    public bool HasUSB { get; set; }
    public bool HasAUX { get; set; }
    public bool HasNavigation { get; set; }
    public bool HasTouchScreen { get; set; }
    public bool HasCarPlay { get; set; }
    public bool HasRearEntertainment { get; set; }
    public bool HasPremiumSound { get; set; }

    // Navigation
    public virtual Listing Listing { get; set; } = null!;
    public virtual VehicleType VehicleType { get; set; } = null!;
    public virtual BodyType? BodyType { get; set; }
    public virtual Brand Brand { get; set; } = null!;
    public virtual Model Model { get; set; } = null!;
    public virtual Package? Package { get; set; }
    public virtual FuelType FuelType { get; set; } = null!;
    public virtual TransmissionType TransmissionType { get; set; } = null!;
}
