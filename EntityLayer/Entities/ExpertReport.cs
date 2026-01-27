using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Entities;

public class ExpertReport
{
    public int Id { get; set; }

    public int QuoteRequestId { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = null!;

    [MaxLength(100)]
    public string? ContentType { get; set; }

    public byte[]? Data { get; set; }

    public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

    public virtual QuoteRequest QuoteRequest { get; set; } = null!;
}
