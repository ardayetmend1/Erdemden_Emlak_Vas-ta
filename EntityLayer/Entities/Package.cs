using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Entities;

public class Package : BaseEntity
{
    public Guid ModelId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    public virtual Model Model { get; set; } = null!;
    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
