using EntityLayer.Entities;

namespace Core.DTOs.ListingDtos;

/// <summary>
/// İlan satış bilgileri (Admin dashboard için)
/// </summary>
public class ListingSaleInfoDto
{
    // Finansal bilgiler
    public decimal? PurchasePrice { get; set; }
    public decimal? Expenses { get; set; }
    public decimal? SalePrice { get; set; }

    /// <summary>
    /// Toplam maliyet = Alış fiyatı + Masraflar
    /// </summary>
    public decimal? TotalCost => PurchasePrice + Expenses;

    /// <summary>
    /// Kar/Zarar = Satış fiyatı - Toplam maliyet
    /// </summary>
    public decimal? ProfitLoss => SalePrice - TotalCost;

    // Satış tarihi
    public DateTime? SoldDate { get; set; }

    // Alıcı bilgileri
    public string? SoldTo { get; set; }
    public string? SoldToPhone { get; set; }
    public string? SoldToEmail { get; set; }
    public BuyerReason? BuyerReason { get; set; }
    public string? BuyerReasonText => BuyerReason?.ToString();
}
