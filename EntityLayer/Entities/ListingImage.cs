using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Entities;

public class ListingImage : BaseEntity
{
    public Guid ListingId { get; set; }

    [Required]
    [MaxLength(500)]
    public string ImageUrl { get; set; } = null!;

    public bool IsCover { get; set; }

    public int Order { get; set; }

    public virtual Listing Listing { get; set; } = null!;
}
