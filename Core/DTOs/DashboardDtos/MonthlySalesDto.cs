namespace Core.DTOs.DashboardDtos;

/// <summary>
/// Aylık satış verisi (Grafik için)
/// </summary>
public class MonthlySalesDto
{
    public int Year { get; set; }
    public int Month { get; set; }

    /// <summary>
    /// Ay adı (örn: "Mart 26")
    /// </summary>
    public string MonthName { get; set; } = string.Empty;

    public int VehicleSales { get; set; }
    public int RealEstateSales { get; set; }
    public int TotalSales => VehicleSales + RealEstateSales;

    // Finansal
    public decimal VehicleRevenue { get; set; }
    public decimal RealEstateRevenue { get; set; }
    public decimal TotalRevenue => VehicleRevenue + RealEstateRevenue;
}
