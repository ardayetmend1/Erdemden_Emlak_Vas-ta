namespace Core.DTOs.DocumentDtos;

/// <summary>
/// Medya dosyası yanıtı (Fotoğraf veya Video)
/// </summary>
public class MediaDocumentDto
{
    public Guid Id { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;

    /// <summary>
    /// "Photo" veya "Video"
    /// </summary>
    public string MediaType { get; set; } = string.Empty;

    public long FileSize { get; set; }
    public string DownloadUrl { get; set; } = string.Empty;
}
