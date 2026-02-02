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
}
