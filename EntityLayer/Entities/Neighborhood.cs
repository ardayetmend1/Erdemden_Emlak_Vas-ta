using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Entities;

public class Neighborhood : BaseEntity
{
    public Guid DistrictId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    public virtual District District { get; set; } = null!;
    public virtual ICollection<Listing> Listings { get; set; } = new List<Listing>();
}
