namespace BussinessLayer.Settings;

/// <summary>
/// JWT konfigürasyon ayarları - appsettings.json'dan okunur
/// </summary>
public class JwtSettings
{
    public const string SectionName = "JwtSettings";

    /// <summary>
    /// Token imzalama için secret key (en az 32 karakter)
    /// </summary>
    public string SecretKey { get; set; } = string.Empty;

    /// <summary>
    /// Token'ı oluşturan (API)
    /// </summary>
    public string Issuer { get; set; } = string.Empty;

    /// <summary>
    /// Token'ı kullanacak (Frontend)
    /// </summary>
    public string Audience { get; set; } = string.Empty;

    /// <summary>
    /// Access token süresi (dakika) - Varsayılan: 15 dakika
    /// </summary>
    public int AccessTokenExpirationMinutes { get; set; } = 15;

    /// <summary>
    /// Refresh token süresi (gün) - Varsayılan: 7 gün
    /// </summary>
    public int RefreshTokenExpirationDays { get; set; } = 7;
}
