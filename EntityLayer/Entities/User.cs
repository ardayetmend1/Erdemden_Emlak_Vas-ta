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

    [Required]
    public string PasswordHash { get; set; } = null!;

    public UserRole Role { get; set; } = UserRole.User;

    public bool IsActive { get; set; } = true;

    public DateTime? LastLoginAt { get; set; }

    public virtual ICollection<UserFavorite> Favorites { get; set; } = new List<UserFavorite>();
}
