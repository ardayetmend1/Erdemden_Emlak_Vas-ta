using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Entities;

public class RealEstate : BaseEntity
{
    [ForeignKey("Listing")]
    public Guid ListingId { get; set; }

    public Guid HousingTypeId { get; set; }

    [MaxLength(20)]
    public string? RoomCount { get; set; }

    public int? Size { get; set; }

    public int? Floor { get; set; }

    public int? TotalFloors { get; set; }

    public int? BuildingAge { get; set; }

    public bool? HasElevator { get; set; }

    public bool? HasParking { get; set; }

    public bool? IsFurnished { get; set; }

    // Navigation
    public virtual Listing Listing { get; set; } = null!;
    public virtual HousingType HousingType { get; set; } = null!;
}
