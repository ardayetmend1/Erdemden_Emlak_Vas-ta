using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.VehicleDtos;

/// <summary>
/// Araç güncelleme isteği
/// </summary>
public class UpdateVehicleDto
{
    [Range(1900, 2100, ErrorMessage = "Geçerli bir yıl giriniz")]
    public int? Year { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Kilometre 0'dan büyük olmalıdır")]
    public int? Km { get; set; }

    [StringLength(50)]
    public string? Color { get; set; }

    public string? DamageStatus { get; set; }

    public Guid? VehicleTypeId { get; set; }
    public Guid? BrandId { get; set; }
    public Guid? ModelId { get; set; }
    public Guid? FuelTypeId { get; set; }
    public Guid? TransmissionTypeId { get; set; }
    public Guid? BodyTypeId { get; set; }
}
