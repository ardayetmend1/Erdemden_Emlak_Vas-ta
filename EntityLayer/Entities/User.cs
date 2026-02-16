using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Entities;

public class User : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(255)]
    public string Email { get; set; } = null!;

    public string? PasswordHash { get; set; }

    [MaxLength(255)]
    public string? GoogleId { get; set; }

    [MaxLength(50)]
    public string? AuthProvider { get; set; }

    public UserRole Role { get; set; } = UserRole.User;

    public bool IsActive { get; set; } = true;

    public bool IsEmailVerified { get; set; } = false;

    [MaxLength(6)]
    public string? EmailVerificationCode { get; set; }

    public DateTime? EmailVerificationCodeExpiry { get; set; }

    public DateTime? LastLoginAt { get; set; }

    public virtual ICollection<UserFavorite> Favorites { get; set; } = new List<UserFavorite>();
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}
