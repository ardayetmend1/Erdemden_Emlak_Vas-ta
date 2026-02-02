using Core.DTOs.Common;

namespace Core.DTOs.RealEstateDtos;

/// <summary>
/// Emlak filtreleme parametreleri
/// </summary>
public class RealEstateFilterDto : PaginationRequestDto
{
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
