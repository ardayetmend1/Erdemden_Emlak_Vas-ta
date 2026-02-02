namespace Core.DTOs.DashboardDtos;

/// <summary>
/// Dashboard genel istatistikleri
/// </summary>
public class DashboardStatsDto
{
    // İlan sayıları
    public int TotalListings { get; set; }
    public int ActiveListings { get; set; }
    public int SoldListings { get; set; }
    public int OptionalListings { get; set; }

    // Kategori bazlı
    public int VehicleListings { get; set; }
    public int RealEstateListings { get; set; }

    // Teklif talepleri
    public int TotalQuoteRequests { get; set; }
    public int UnreadQuoteRequests { get; set; }

    // Kullanıcılar
    public int TotalUsers { get; set; }

    // Finansal
    public decimal TotalPurchaseAmount { get; set; }
    public decimal TotalSaleAmount { get; set; }
    public decimal TotalExpenses { get; set; }
    public decimal TotalProfit => TotalSaleAmount - TotalPurchaseAmount - TotalExpenses;
}
