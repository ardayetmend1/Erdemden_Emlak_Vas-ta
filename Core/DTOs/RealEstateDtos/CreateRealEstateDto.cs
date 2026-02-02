using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.RealEstateDtos;

/// <summary>
/// Emlak oluşturma isteği (Listing ile birlikte)
/// </summary>
public class CreateRealEstateDto
{
    [Required(ErrorMessage = "Oda sayısı gereklidir")]
    [StringLength(20)]
    public string RoomCount { get; set; } = string.Empty;

    [Required(ErrorMessage = "Metrekare gereklidir")]
    [Range(1, int.MaxValue, ErrorMessage = "Metrekare 0'dan büyük olmalıdır")]
    public int Size { get; set; }

    public int? Floor { get; set; }

    public int? TotalFloors { get; set; }

    public int? BuildingAge { get; set; }

    public bool HasElevator { get; set; }

    public bool HasParking { get; set; }

    public bool IsFurnished { get; set; }

    [Required(ErrorMessage = "Emlak tipi gereklidir")]
    public Guid HousingTypeId { get; set; }
}
