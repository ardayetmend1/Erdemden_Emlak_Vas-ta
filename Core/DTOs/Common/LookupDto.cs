namespace Core.DTOs.Common;

/// <summary>
/// Basit lookup entity'ler için DTO (FuelType, TransmissionType, VehicleType, HousingType, City, Brand)
/// </summary>
public class LookupDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
