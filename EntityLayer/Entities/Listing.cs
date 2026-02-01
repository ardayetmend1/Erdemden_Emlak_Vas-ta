using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Entities;

public class Listing : BaseEntity
{
    [Required]
    [MaxLength(255)]
    public string Title { get; set; } = null!;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [MaxLength(10)]
    public string Currency { get; set; } = "TL";

    [MaxLength(500)]
    public string? ImageUrl { get; set; }

    public ListingCategory Category { get; set; }

    [MaxLength(2000)]
    public string? Description { get; set; }

    public ListingStatus Status { get; set; } = ListingStatus.Satilik;

    public DateTime ListingDate { get; set; } = DateTime.UtcNow;

    // Konum
    public Guid CityId { get; set; }
    public Guid? DistrictId { get; set; }

    // Satış Bilgileri (Admin)
    [Column(TypeName = "decimal(18,2)")]
    public decimal? PurchasePrice { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? SalePrice { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal? Expenses { get; set; }

    public DateTime? SoldDate { get; set; }

    [MaxLength(255)]
    public string? SoldTo { get; set; }

    [MaxLength(20)]
    public string? SoldToPhone { get; set; }

    [MaxLength(255)]
    public string? SoldToEmail { get; set; }

    public BuyerReason? BuyerReason { get; set; }

    // Navigation
    public virtual City City { get; set; } = null!;
    public virtual District? District { get; set; }
    public virtual Vehicle? Vehicle { get; set; }
    public virtual RealEstate? RealEstate { get; set; }
    public virtual ICollection<ListingImage> Images { get; set; } = new List<ListingImage>();
    public virtual ICollection<NotaryDocument> NotaryDocuments { get; set; } = new List<NotaryDocument>();
    public virtual ICollection<UserFavorite> FavoritedBy { get; set; } = new List<UserFavorite>();
}
