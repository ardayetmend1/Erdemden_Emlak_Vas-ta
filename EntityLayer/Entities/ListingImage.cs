using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Entities;

public class ListingImage
{
    public int Id { get; set; }

    public int ListingId { get; set; }

    [Required]
    [MaxLength(500)]
    public string Url { get; set; } = null!;

    public int Order { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual Listing Listing { get; set; } = null!;
}
