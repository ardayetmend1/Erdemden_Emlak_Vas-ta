using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Entities;

public class ListingImage : BaseEntity
{
    public Guid ListingId { get; set; }

    /// <summary>
    /// Görsel URL'i (harici link veya base64 data URI)
    /// </summary>
    [MaxLength(500)]
    public string? ImageUrl { get; set; }

    /// <summary>
    /// Base64 encoded görsel verisi
    /// </summary>
    public string? Base64Data { get; set; }

    /// <summary>
    /// Görsel mime tipi (image/jpeg, image/png vb.)
    /// </summary>
    [MaxLength(50)]
    public string? MimeType { get; set; }

    /// <summary>
    /// Orijinal dosya adı
    /// </summary>
    [MaxLength(255)]
    public string? FileName { get; set; }

    public bool IsCover { get; set; }

    public int Order { get; set; }

    public virtual Listing Listing { get; set; } = null!;

    /// <summary>
    /// Görsel URL'ini döndürür (Base64 varsa data URI, yoksa ImageUrl)
    /// </summary>
    public string GetDisplayUrl()
    {
        if (!string.IsNullOrEmpty(Base64Data) && !string.IsNullOrEmpty(MimeType))
        {
            return $"data:{MimeType};base64,{Base64Data}";
        }
        return ImageUrl ?? string.Empty;
    }
}
