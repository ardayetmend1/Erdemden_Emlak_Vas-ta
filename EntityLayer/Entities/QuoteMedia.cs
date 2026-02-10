using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Entities;

public class QuoteMedia : BaseEntity
{
    public Guid QuoteRequestId { get; set; }

    [Required]
    [MaxLength(255)]
    public string FileName { get; set; } = null!;

    [MaxLength(100)]
    public string ContentType { get; set; } = string.Empty;

    /// <summary>
    /// "Photo" veya "Video"
    /// </summary>
    [MaxLength(10)]
    public string MediaType { get; set; } = string.Empty;

    /// <summary>
    /// Fotoğraflar için binary data (DB'de saklanır)
    /// </summary>
    public byte[]? Data { get; set; }

    /// <summary>
    /// Videolar için dosya yolu (dosya sisteminde saklanır)
    /// </summary>
    [MaxLength(500)]
    public string? FilePath { get; set; }

    public long FileSize { get; set; }

    public virtual QuoteRequest QuoteRequest { get; set; } = null!;
}
