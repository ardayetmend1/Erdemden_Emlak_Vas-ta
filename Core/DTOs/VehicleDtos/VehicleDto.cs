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
}
