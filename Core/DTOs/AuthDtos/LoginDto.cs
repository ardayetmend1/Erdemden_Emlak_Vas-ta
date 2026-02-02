using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.AuthDtos;

/// <summary>
/// Kullanıcı giriş isteği
/// </summary>
public class LoginDto
{
    [Required(ErrorMessage = "E-posta adresi gereklidir")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre gereklidir")]
    public string Password { get; set; } = string.Empty;
}
