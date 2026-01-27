using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Entities;

public class City
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    public virtual ICollection<District> Districts { get; set; } = new List<District>();
    public virtual ICollection<Listing> Listings { get; set; } = new List<Listing>();
}
