using System.ComponentModel.DataAnnotations;
using EntityLayer.Entities;

namespace Core.DTOs.ListingDtos;

/// <summary>
/// İlan oluşturma isteği (Araç veya Emlak için temel bilgiler)
/// </summary>
public class CreateListingDto
{
    [Required(ErrorMessage = "İlan başlığı gereklidir")]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "Başlık 5-200 karakter arasında olmalıdır")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Fiyat gereklidir")]
    [Range(0, double.MaxValue, ErrorMessage = "Fiyat 0'dan büyük olmalıdır")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "Para birimi gereklidir")]
    public string Currency { get; set; } = "TRY";

    public string? Description { get; set; }

    [Required(ErrorMessage = "Kategori gereklidir")]
    public ListingCategory Category { get; set; }

    public ListingStatus Status { get; set; } = ListingStatus.Satilik;

    [Required(ErrorMessage = "Şehir gereklidir")]
    public Guid CityId { get; set; }

    public Guid? DistrictId { get; set; }

    /// <summary>
    /// Admin satın alma fiyatı
    /// </summary>
    public decimal? PurchasePrice { get; set; }

    /// <summary>
    /// Masraflar
    /// </summary>
    public decimal? Expenses { get; set; }
}
