using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Entities;

public class QuoteRequest : BaseEntity
{
    public DateTime Date { get; set; } = DateTime.UtcNow;

    // Araç bilgisi
    [MaxLength(20)]
    public string? Plate { get; set; }

    [MaxLength(100)]
    public string? Brand { get; set; }

    [MaxLength(100)]
    public string? Model { get; set; }

    [MaxLength(10)]
    public string? Year { get; set; }

    [MaxLength(20)]
    public string? Km { get; set; }

    [MaxLength(50)]
    public string? Gear { get; set; }

    [MaxLength(50)]
    public string? Fuel { get; set; }

    [MaxLength(500)]
    public string? Damage { get; set; }

    // Müşteri bilgisi
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = null!;

    [Required]
    [MaxLength(20)]
    public string Phone { get; set; } = null!;

    [MaxLength(255)]
    public string? Email { get; set; }

    public bool IsRead { get; set; } = false;

    public virtual ICollection<ExpertReport> ExpertReports { get; set; } = new List<ExpertReport>();
    public virtual ICollection<QuoteMedia> Media { get; set; } = new List<QuoteMedia>();
}
