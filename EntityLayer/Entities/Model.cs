using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Entities;

public class Model : BaseEntity
{
    public Guid BrandId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    public virtual Brand Brand { get; set; } = null!;
    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
