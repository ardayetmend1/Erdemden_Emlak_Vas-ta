using System.ComponentModel.DataAnnotations;
using Core.DTOs.ImageDtos;
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
    [StringLength(10)]
    public string Currency { get; set; } = "TRY";

    [StringLength(2000)]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Kategori gereklidir")]
    [EnumDataType(typeof(ListingCategory), ErrorMessage = "Geçersiz kategori değeri")]
    public ListingCategory Category { get; set; }

    [EnumDataType(typeof(ListingStatus), ErrorMessage = "Geçersiz durum değeri")]
    public ListingStatus Status { get; set; } = ListingStatus.Satilik;

    [Required(ErrorMessage = "Şehir gereklidir")]
    public Guid CityId { get; set; }

    public Guid? DistrictId { get; set; }

    /// <summary>
    /// Admin satın alma fiyatı
    /// </summary>
    [Range(0, (double)decimal.MaxValue, ErrorMessage = "Alış fiyatı 0'dan büyük olmalıdır")]
    public decimal? PurchasePrice { get; set; }

    /// <summary>
    /// Masraflar
    /// </summary>
    [Range(0, (double)decimal.MaxValue, ErrorMessage = "Masraf 0'dan büyük olmalıdır")]
    public decimal? Expenses { get; set; }

    /// <summary>
    /// İlan görselleri (Base64 formatında)
    /// </summary>
    public List<UploadImageDto>? Images { get; set; }
}
