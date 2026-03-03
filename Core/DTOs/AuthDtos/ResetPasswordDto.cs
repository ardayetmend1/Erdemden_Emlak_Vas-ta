using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.AuthDtos;

/// <summary>
/// Şifre sıfırlama isteği - kod ile doğrulama
/// </summary>
public class ResetPasswordDto
{
    [Required(ErrorMessage = "E-posta adresi gereklidir")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Doğrulama kodu gereklidir")]
    [StringLength(6, MinimumLength = 6, ErrorMessage = "Kod 6 haneli olmalıdır")]
    public string Code { get; set; } = string.Empty;

    [Required(ErrorMessage = "Yeni şifre gereklidir")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Şifre en az 6 karakter olmalıdır")]
    public string NewPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre tekrarı gereklidir")]
    [Compare("NewPassword", ErrorMessage = "Şifreler eşleşmiyor")]
    public string ConfirmNewPassword { get; set; } = string.Empty;
}
