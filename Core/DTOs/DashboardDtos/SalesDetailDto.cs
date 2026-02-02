using Core.DTOs.DocumentDtos;
using EntityLayer.Entities;

namespace Core.DTOs.DashboardDtos;

/// <summary>
/// Satış detay modalı için
/// </summary>
public class SalesDetailDto
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
    public decimal SalePrice { get; set; }
    public decimal TotalCost => PurchasePrice + Expenses;
    public decimal NetProfit => SalePrice - TotalCost;

    // Alıcı bilgileri
    public string? BuyerName { get; set; }
    public string? BuyerPhone { get; set; }
    public string? BuyerEmail { get; set; }
    public BuyerReason? BuyerReason { get; set; }
    public string? BuyerReasonText => BuyerReason?.ToString();

    // Noter belgeleri
    public List<DocumentDto> NotaryDocuments { get; set; } = new();
}
