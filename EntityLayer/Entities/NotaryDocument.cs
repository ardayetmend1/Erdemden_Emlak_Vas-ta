using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Entities;

public class NotaryDocument : BaseEntity
{
    public Guid ListingId { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = null!;

    [MaxLength(100)]
    public string? ContentType { get; set; }

    public byte[]? Data { get; set; }

    public virtual Listing Listing { get; set; } = null!;
}
