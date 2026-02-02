using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.DocumentDtos;

/// <summary>
/// Belge yükleme isteği (Noter belgesi veya Ekspertiz raporu)
/// </summary>
public class UploadDocumentDto
{
    /// <summary>
    /// Base64 encoded belge verisi
    /// </summary>
    [Required(ErrorMessage = "Belge verisi gereklidir")]
    public string Base64Data { get; set; } = string.Empty;

    /// <summary>
    /// Dosya adı (uzantı dahil)
    /// </summary>
    [Required(ErrorMessage = "Dosya adı gereklidir")]
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// İçerik tipi (örn: application/pdf, image/jpeg)
    /// </summary>
    [Required(ErrorMessage = "İçerik tipi gereklidir")]
    public string ContentType { get; set; } = string.Empty;
}
