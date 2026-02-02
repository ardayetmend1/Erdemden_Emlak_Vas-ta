using System.ComponentModel.DataAnnotations;
using EntityLayer.Entities;

namespace Core.DTOs.UserDtos;

/// <summary>
/// Kullanıcı güncelleme isteği
/// </summary>
public class UpdateUserDto
{
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Ad Soyad 2-100 karakter arasında olmalıdır")]
    public string? Name { get; set; }

    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
    public string? Email { get; set; }

    public UserRole? Role { get; set; }

    public bool? IsActive { get; set; }
}
