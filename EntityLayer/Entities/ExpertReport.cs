using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Entities;

public class ExpertReport : BaseEntity
{
    public Guid QuoteRequestId { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; } = null!;

    [MaxLength(100)]
    public string? ContentType { get; set; }

    public byte[]? Data { get; set; }

    public virtual QuoteRequest QuoteRequest { get; set; } = null!;
}
