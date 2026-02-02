using EntityLayer.Entities;

namespace BussinessLayer.Abstract;

/// <summary>
/// Token oluşturma ve doğrulama servisi
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Access token oluşturur (JWT)
    /// </summary>
    string GenerateAccessToken(User user);

    /// <summary>
    /// Refresh token oluşturur (random string)
    /// </summary>
    RefreshToken GenerateRefreshToken(Guid userId);

    /// <summary>
    /// Access token'dan kullanıcı ID'sini çıkarır
    /// </summary>
    Guid? GetUserIdFromToken(string token);

    /// <summary>
    /// Token'ın geçerli olup olmadığını kontrol eder
    /// </summary>
    bool ValidateToken(string token);
}
