namespace Core.DTOs.QuoteRequestDtos;

/// <summary>
/// Müşteri teklif yanıtı DTO'su (Kabul/Red)
/// </summary>
public class RespondToOfferDto
{
    /// <summary>
    /// true = Kabul, false = Red
    /// </summary>
    public bool Accepted { get; set; }
}
