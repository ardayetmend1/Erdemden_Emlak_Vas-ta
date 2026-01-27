using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Entities;

public class BodyType : BaseEntity
{
    public Guid VehicleTypeId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    public virtual VehicleType VehicleType { get; set; } = null!;
    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
