using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Entities;

public class ListingImage : BaseEntity
{
    public Guid ListingId { get; set; }

    [Required]
    [MaxLength(500)]
    public string Url { get; set; } = null!;

    public int Order { get; set; }

    public virtual Listing Listing { get; set; } = null!;
}
