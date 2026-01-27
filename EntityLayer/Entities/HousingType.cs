using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Entities;

public class HousingType : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    public virtual ICollection<RealEstate> RealEstates { get; set; } = new List<RealEstate>();
}
