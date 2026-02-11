using Core.DTOs.Common;

namespace Core.DTOs.VehicleDtos;

/// <summary>
/// Araç detay yanıtı
/// </summary>
public class VehicleDto
{
    public int Year { get; set; }
    public int Km { get; set; }
    public string? Color { get; set; }
    public string? DamageStatus { get; set; }

    // Lookup bilgileri (Id ve Name)
    public LookupDto VehicleType { get; set; } = null!;
    public LookupDto Brand { get; set; } = null!;
    public LookupWithParentDto Model { get; set; } = null!;
    public LookupDto FuelType { get; set; } = null!;
    public LookupDto TransmissionType { get; set; } = null!;
    public LookupWithParentDto? BodyType { get; set; }
    public LookupWithParentDto? Package { get; set; }

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
