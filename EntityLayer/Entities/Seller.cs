using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Entities;

public class Seller : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    [MaxLength(20)]
    public string? Phone { get; set; }

    [MaxLength(500)]
    public string? Avatar { get; set; }

    public virtual ICollection<Listing> Listings { get; set; } = new List<Listing>();
}
