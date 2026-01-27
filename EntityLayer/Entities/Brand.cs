using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Entities;

public class Brand
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    public virtual ICollection<Model> Models { get; set; } = new List<Model>();
    public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
