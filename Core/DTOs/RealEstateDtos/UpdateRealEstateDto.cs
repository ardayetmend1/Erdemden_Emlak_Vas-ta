using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.RealEstateDtos;

/// <summary>
/// Emlak güncelleme isteği
/// </summary>
public class UpdateRealEstateDto
{
    [StringLength(20)]
    public string? RoomCount { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Metrekare 0'dan büyük olmalıdır")]
    public int? Size { get; set; }

    public int? Floor { get; set; }

    public int? TotalFloors { get; set; }

    public int? BuildingAge { get; set; }

    public bool? HasElevator { get; set; }

    public bool? HasParking { get; set; }

    public bool? IsFurnished { get; set; }

    public Guid? HousingTypeId { get; set; }

    /// <summary>
    /// 0 = Satılık, 1 = Kiralık
    /// </summary>
    public int? ListingType { get; set; }

    /// <summary>
    /// Aylık kira ücreti (Kiralık için)
    /// </summary>
    public decimal? MonthlyRent { get; set; }

    /// <summary>
    /// Depozito (Kiralık için)
    /// </summary>
    public decimal? Deposit { get; set; }
}
