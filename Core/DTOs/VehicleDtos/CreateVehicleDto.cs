using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.VehicleDtos;

/// <summary>
/// Araç oluşturma isteği (Listing ile birlikte)
/// </summary>
public class CreateVehicleDto
{
    [Required(ErrorMessage = "Yıl gereklidir")]
    [Range(1900, 2100, ErrorMessage = "Geçerli bir yıl giriniz")]
    public int Year { get; set; }

    [Required(ErrorMessage = "Kilometre gereklidir")]
    [Range(0, int.MaxValue, ErrorMessage = "Kilometre 0'dan büyük olmalıdır")]
    public int Km { get; set; }

    [StringLength(50)]
    public string? Color { get; set; }

    [StringLength(255)]
    public string? DamageStatus { get; set; }

    [Required(ErrorMessage = "Araç tipi gereklidir")]
    public Guid VehicleTypeId { get; set; }

    [Required(ErrorMessage = "Marka gereklidir")]
    public Guid BrandId { get; set; }

    [Required(ErrorMessage = "Model gereklidir")]
    public Guid ModelId { get; set; }

    [Required(ErrorMessage = "Yakıt tipi gereklidir")]
    public Guid FuelTypeId { get; set; }

    [Required(ErrorMessage = "Vites tipi gereklidir")]
    public Guid TransmissionTypeId { get; set; }

    public Guid? BodyTypeId { get; set; }
    public Guid? PackageId { get; set; }

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
}
