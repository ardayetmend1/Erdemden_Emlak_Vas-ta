using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.QuoteRequestDtos;

/// <summary>
/// Teklif talebi oluşturma (Kullanıcı formu gönderir)
/// </summary>
public class CreateQuoteRequestDto
{
    // Araç bilgileri
    [Required(ErrorMessage = "Plaka gereklidir")]
    [StringLength(20)]
    public string Plate { get; set; } = string.Empty;

    [Required(ErrorMessage = "Marka gereklidir")]
    [StringLength(100)]
    public string Brand { get; set; } = string.Empty;

    [Required(ErrorMessage = "Model gereklidir")]
    [StringLength(100)]
    public string Model { get; set; } = string.Empty;

    [StringLength(10)]
    public string? Year { get; set; }

    [StringLength(20)]
    public string? Km { get; set; }

    [StringLength(50)]
    public string? Gear { get; set; }

    [StringLength(50)]
    public string? Fuel { get; set; }

    public string? Damage { get; set; }

    // İletişim bilgileri
    [Required(ErrorMessage = "Ad gereklidir")]
    [StringLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Soyad gereklidir")]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Telefon gereklidir")]
    [Phone(ErrorMessage = "Geçerli bir telefon numarası giriniz")]
    public string Phone { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
    public string? Email { get; set; }
}
