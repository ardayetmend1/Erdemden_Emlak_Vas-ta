using EntityLayer.Entities;

namespace Core.DTOs.DashboardDtos;

/// <summary>
/// Satış raporu tablosu satırı
/// </summary>
public class SalesReportDto
{
    public Guid ListingId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? ImageUrl { get; set; }
    public ListingCategory Category { get; set; }

    // Tarih
    public DateTime? SoldDate { get; set; }

    // Finansal
    public decimal PurchasePrice { get; set; }
    public decimal Expenses { get; set; }
    public decimal TotalCost => PurchasePrice + Expenses;
    public decimal SalePrice { get; set; }
    public decimal ProfitLoss => SalePrice - TotalCost;
    public bool IsProfit => ProfitLoss >= 0;
}
