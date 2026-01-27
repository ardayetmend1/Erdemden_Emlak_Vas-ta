using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Entities;

public class VehicleType : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    public virtual ICollection<BodyType> BodyTypes { get; set; } = new List<BodyType>();
    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
