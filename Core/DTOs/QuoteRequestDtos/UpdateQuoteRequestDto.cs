namespace Core.DTOs.QuoteRequestDtos;

/// <summary>
/// Teklif talebi güncelleme (Admin: Okundu işareti)
/// </summary>
public class UpdateQuoteRequestDto
{
    public bool? IsRead { get; set; }
}
