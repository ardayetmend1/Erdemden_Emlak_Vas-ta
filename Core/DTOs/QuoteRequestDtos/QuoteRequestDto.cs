using Core.DTOs.DocumentDtos;

namespace Core.DTOs.QuoteRequestDtos;

/// <summary>
/// Teklif talebi yanıtı (Liste görünümü)
/// </summary>
public class QuoteRequestDto
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public bool IsRead { get; set; }

    // Araç bilgileri
    public string Plate { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string? Year { get; set; }
    public string? Km { get; set; }
    public string? Gear { get; set; }
    public string? Fuel { get; set; }
    public string? Damage { get; set; }

    // İletişim bilgileri
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
    public string Phone { get; set; } = string.Empty;
    public string? Email { get; set; }

    // Ekspertiz raporları
    public List<DocumentDto> ExpertReports { get; set; } = new();

    // Medya dosyaları
    public List<MediaDocumentDto> Photos { get; set; } = new();
    public List<MediaDocumentDto> Videos { get; set; } = new();
}
