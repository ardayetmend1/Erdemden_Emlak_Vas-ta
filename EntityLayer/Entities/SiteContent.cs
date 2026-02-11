using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Entities;

public class SiteContent : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string Key { get; set; } = null!;

    public string? Image { get; set; }

    [MaxLength(255)]
    public string? Title { get; set; }

    public string? Description { get; set; }
}
