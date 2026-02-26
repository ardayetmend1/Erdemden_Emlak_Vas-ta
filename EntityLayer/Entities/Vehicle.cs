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

    /// <summary>Motor gücü (HP/BG)</summary>
    public int? EnginePower { get; set; }

    /// <summary>Motor hacmi (cc)</summary>
    public int? EngineDisplacement { get; set; }

    [MaxLength(50)]
    public string? Color { get; set; }

    [MaxLength(255)]
    public string? DamageStatus { get; set; }

    /// <summary>Tramer durumu: 0=Bilmiyorum, 1=Tramer Yok, 2=Tramer Var, 3=Ağır Hasarlı</summary>
    public int TramerStatus { get; set; } = 0;

    /// <summary>Tramer tutarı (TL) - sadece TramerStatus=2 ise anlamlı</summary>
    [Column(TypeName = "decimal(18,2)")]
    public decimal? TramerAmount { get; set; }

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
    public bool HasAEB { get; set; }
    public bool HasBAS { get; set; }
    public bool HasDistronic { get; set; }
    public bool HasNightVision { get; set; }
    public bool HasDriverAirbag { get; set; }
    public bool HasPassengerAirbag { get; set; }
    public bool HasChildLock { get; set; }
    public bool HasHillAssist { get; set; }
    public bool HasFatigueDetection { get; set; }
    public bool HasArmoredVehicle { get; set; }

    // ==================== İÇ DONANIM ====================
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
    public bool HasAdaptiveCruiseControl { get; set; }
    public bool HasKeylessEntry { get; set; }
    public bool HasFunctionalSteering { get; set; }
    public bool HasHeatedSteering { get; set; }
    public bool HasHydraulicSteering { get; set; }
    public bool HasHeadUpDisplay { get; set; }
    public bool HasSpeedLimiter { get; set; }
    public bool HasMemorySeats { get; set; }
    public bool HasSeatCooling { get; set; }
    public bool HasFabricSeats { get; set; }
    public bool HasElectricSeats { get; set; }
    public bool HasAutoDimmingMirror { get; set; }
    public bool HasFrontCamera { get; set; }
    public bool HasArmrest { get; set; }
    public bool HasCooledGlovebox { get; set; }
    public bool HasThirdRowSeats { get; set; }
    public bool HasTripComputer { get; set; }

    // ==================== DIŞ DONANIM ====================
    public bool HasFootTrunkOpener { get; set; }
    public bool HasHardtop { get; set; }
    public bool HasAdaptiveLights { get; set; }
    public bool HasElectricFoldMirrors { get; set; }
    public bool HasHeatedMirrors { get; set; }
    public bool HasMemoryMirrors { get; set; }
    public bool HasRearParkSensor { get; set; }
    public bool HasFrontParkSensor { get; set; }
    public bool HasParkAssist { get; set; }
    public bool HasSmartTrunk { get; set; }
    public bool HasPanoramicRoof { get; set; }
    public bool HasTowBar { get; set; }

    // ==================== MULTİMEDYA ====================
    public bool HasBluetooth { get; set; }
    public bool HasUSB { get; set; }
    public bool HasAUX { get; set; }
    public bool HasNavigation { get; set; }
    public bool HasTouchScreen { get; set; }
    public bool HasCarPlay { get; set; }
    public bool HasRearEntertainment { get; set; }
    public bool HasPremiumSound { get; set; }
    public bool HasAndroidAuto { get; set; }

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
