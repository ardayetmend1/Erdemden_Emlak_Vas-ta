using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.AuthDtos;

public class GoogleLoginDto
{
    [Required(ErrorMessage = "Google ID token gereklidir")]
    public string IdToken { get; set; } = string.Empty;
}
