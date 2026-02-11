using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Entities;

public class Model : BaseEntity
{
    public Guid BrandId { get; set; }

    public Guid? BodyTypeId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    public virtual Brand Brand { get; set; } = null!;
    public virtual BodyType? BodyType { get; set; }
    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    public virtual ICollection<Package> Packages { get; set; } = new List<Package>();
}
