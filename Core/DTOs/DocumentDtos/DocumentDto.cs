namespace Core.DTOs.DocumentDtos;

/// <summary>
/// Belge yanıtı (Noter belgesi veya Ekspertiz raporu)
/// </summary>
public class DocumentDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ContentType { get; set; } = string.Empty;

    /// <summary>
    /// İndirme URL'i (API endpoint)
    /// </summary>
    public string DownloadUrl { get; set; } = string.Empty;
}
