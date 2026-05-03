using Core.DTOs.Common;
using EntityLayer.Entities;

namespace Core.DTOs.RealEstateDtos;

/// <summary>
/// Emlak filtreleme parametreleri
/// </summary>
public class RealEstateFilterDto : PaginationRequestDto
{
    /// <summary>
    /// Konut / İş Yeri / Arsa filtresi
    /// </summary>
    public RealEstateCategory? Category { get; set; }

    public Guid? HousingTypeId { get; set; }

    // Oda sayısı
    public string? RoomCount { get; set; }

    // Metrekare aralığı
    public int? MinSize { get; set; }
    public int? MaxSize { get; set; }

    // Kat
    public int? MinFloor { get; set; }
    public int? MaxFloor { get; set; }

    // Bina yaşı
    public int? MaxBuildingAge { get; set; }

    // Özellikler
    public bool? HasElevator { get; set; }
    public bool? HasParking { get; set; }
    public bool? IsFurnished { get; set; }
}
