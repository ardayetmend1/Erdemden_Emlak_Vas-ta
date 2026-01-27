using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Entities;

public class FuelType
{
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;

    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
