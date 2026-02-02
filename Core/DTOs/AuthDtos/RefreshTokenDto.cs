using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.AuthDtos;

/// <summary>
/// Token yenileme isteği
/// </summary>
public class RefreshTokenDto
{
    [Required(ErrorMessage = "Access token gereklidir")]
    public string AccessToken { get; set; } = string.Empty;

    [Required(ErrorMessage = "Refresh token gereklidir")]
    public string RefreshToken { get; set; } = string.Empty;
}
