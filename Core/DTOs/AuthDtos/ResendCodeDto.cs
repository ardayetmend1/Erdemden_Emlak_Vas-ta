using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.AuthDtos;

public class ResendCodeDto
{
    [Required(ErrorMessage = "E-posta adresi gereklidir")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
    public string Email { get; set; } = string.Empty;
}
