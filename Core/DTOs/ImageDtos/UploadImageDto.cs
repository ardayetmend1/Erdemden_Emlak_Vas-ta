using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.ImageDtos;

/// <summary>
/// Görsel yükleme isteği
/// </summary>
public class UploadImageDto
{
    /// <summary>
    /// Base64 encoded görsel verisi
    /// </summary>
    [Required(ErrorMessage = "Görsel verisi gereklidir")]
    public string Base64Data { get; set; } = string.Empty;

    /// <summary>
    /// Dosya adı (uzantı dahil)
    /// </summary>
    [Required(ErrorMessage = "Dosya adı gereklidir")]
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// Sıralama (opsiyonel)
    /// </summary>
    public int? Order { get; set; }
}
