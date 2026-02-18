using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.QuoteRequestDtos;

/// <summary>
/// Admin teklif verme DTO'su (fiyat aralığı)
/// </summary>
public class SubmitOfferDto
{
    [Required(ErrorMessage = "Minimum fiyat gereklidir")]
    [Range(1, double.MaxValue, ErrorMessage = "Minimum fiyat 0'dan büyük olmalıdır")]
    public decimal MinPrice { get; set; }

    [Required(ErrorMessage = "Maksimum fiyat gereklidir")]
    [Range(1, double.MaxValue, ErrorMessage = "Maksimum fiyat 0'dan büyük olmalıdır")]
    public decimal MaxPrice { get; set; }
}
