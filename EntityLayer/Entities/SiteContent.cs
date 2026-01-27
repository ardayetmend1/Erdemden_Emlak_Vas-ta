using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Entities;

public class SiteContent : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public string Key { get; set; } = null!;

    [MaxLength(500)]
    public string? Image { get; set; }

    [MaxLength(255)]
    public string? Title { get; set; }

    [MaxLength(1000)]
    public string? Description { get; set; }
}
