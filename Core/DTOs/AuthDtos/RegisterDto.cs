using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.AuthDtos;

/// <summary>
/// Kullanıcı kayıt isteği
/// </summary>
public class RegisterDto
{
    [Required(ErrorMessage = "Ad Soyad gereklidir")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Ad Soyad 2-100 karakter arasında olmalıdır")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "E-posta adresi gereklidir")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre gereklidir")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Şifre en az 6 karakter olmalıdır")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre tekrarı gereklidir")]
    [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
