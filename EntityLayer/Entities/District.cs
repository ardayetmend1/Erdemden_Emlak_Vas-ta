using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Entities;

public class District : BaseEntity
{
    public Guid CityId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    public virtual City City { get; set; } = null!;
    public virtual ICollection<Listing> Listings { get; set; } = new List<Listing>();
}
