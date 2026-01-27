using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Entities;

public class Vehicle
{
    [Key]
    [ForeignKey("Listing")]
    public int ListingId { get; set; }

    public int VehicleTypeId { get; set; }
    public int? BodyTypeId { get; set; }
    public int BrandId { get; set; }
    public int ModelId { get; set; }
    public int FuelTypeId { get; set; }
    public int TransmissionTypeId { get; set; }

    public int? Year { get; set; }
    public int? Km { get; set; }

    [MaxLength(50)]
    public string? Color { get; set; }

    [MaxLength(255)]
    public string? DamageStatus { get; set; }

    // Navigation
    public virtual Listing Listing { get; set; } = null!;
    public virtual VehicleType VehicleType { get; set; } = null!;
    public virtual BodyType? BodyType { get; set; }
    public virtual Brand Brand { get; set; } = null!;
    public virtual Model Model { get; set; } = null!;
    public virtual FuelType FuelType { get; set; } = null!;
    public virtual TransmissionType TransmissionType { get; set; } = null!;
}
