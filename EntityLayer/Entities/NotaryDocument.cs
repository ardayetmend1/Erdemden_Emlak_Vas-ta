using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Entities;

public class NotaryDocument
{
    public int Id { get; set; }

    public int ListingId { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = null!;

    [MaxLength(100)]
    public string? ContentType { get; set; }

    public byte[]? Data { get; set; }

    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

    public virtual Listing Listing { get; set; } = null!;
}
