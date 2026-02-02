using Core.DTOs.Common;

namespace Core.DTOs.VehicleDtos;

/// <summary>
/// Araç filtreleme parametreleri
/// </summary>
public class VehicleFilterDto : PaginationRequestDto
{
    // Araç tipi, marka, model
    public Guid? VehicleTypeId { get; set; }
    public Guid? BrandId { get; set; }
    public Guid? ModelId { get; set; }

    // Yıl aralığı
    public int? MinYear { get; set; }
    public int? MaxYear { get; set; }

    // Kilometre aralığı
    public int? MinKm { get; set; }
    public int? MaxKm { get; set; }

    // Diğer filtreler
    public Guid? FuelTypeId { get; set; }
    public Guid? TransmissionTypeId { get; set; }
    public Guid? BodyTypeId { get; set; }
    public string? Color { get; set; }
}
