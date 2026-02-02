using System.ComponentModel.DataAnnotations;
using EntityLayer.Entities;

namespace Core.DTOs.ListingDtos;

/// <summary>
/// İlan güncelleme isteği (Satış bilgileri dahil)
/// </summary>
public class UpdateListingDto
{
    [StringLength(200, MinimumLength = 5, ErrorMessage = "Başlık 5-200 karakter arasında olmalıdır")]
    public string? Title { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Fiyat 0'dan büyük olmalıdır")]
    public decimal? Price { get; set; }

    public string? Currency { get; set; }

    public string? Description { get; set; }

    public ListingStatus? Status { get; set; }

    public Guid? CityId { get; set; }

    public Guid? DistrictId { get; set; }

    // Admin satış bilgileri
    public decimal? PurchasePrice { get; set; }
    public decimal? Expenses { get; set; }
    public decimal? SalePrice { get; set; }
    public DateTime? SoldDate { get; set; }

    // Alıcı bilgileri
    public string? SoldTo { get; set; }
    public string? SoldToPhone { get; set; }
    public string? SoldToEmail { get; set; }
    public BuyerReason? BuyerReason { get; set; }
}
