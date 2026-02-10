using BussinessLayer.Abstract;
using BussinessLayer.Settings;
using Core.DTOs.AuthDtos;
using Core.DTOs.Common;
using Core.DTOs.UserDtos;
using DataAcessLayer.Abstract;
using EntityLayer.Entities;
using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BussinessLayer.Concrete;

/// <summary>
/// Kimlik doğrulama servisi implementasyonu
/// </summary>
public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly JwtSettings _jwtSettings;

    public AuthService(IUnitOfWork unitOfWork, ITokenService tokenService, IOptions<JwtSettings> jwtSettings)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _jwtSettings = jwtSettings.Value;
    }

    /// <summary>
    /// Kullanıcı girişi
    /// </summary>
    public async Task<ApiResponseDto<AuthResponseDto>> LoginAsync(LoginDto loginDto)
    {
        var user = await _unitOfWork.Repository<User>()
            .GetAsync(u => u.Email == loginDto.Email);

        if (user == null)
        {
            return ApiResponseDto<AuthResponseDto>.FailResponse("E-posta veya şifre hatalı");
        }

        if (string.IsNullOrEmpty(user.PasswordHash))
        {
            return ApiResponseDto<AuthResponseDto>.FailResponse("Bu hesap Google ile oluşturuldu. Lütfen Google ile giriş yapın.");
        }

        if (!VerifyPassword(loginDto.Password, user.PasswordHash))
        {
            return ApiResponseDto<AuthResponseDto>.FailResponse("E-posta veya şifre hatalı");
        }

        if (!user.IsActive)
        {
            return ApiResponseDto<AuthResponseDto>.FailResponse("Hesabınız devre dışı bırakılmış");
        }

        // Token oluştur
        var accessToken = _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken(user.Id);

        // Refresh token'ı kaydet
        await _unitOfWork.Repository<RefreshToken>().AddAsync(refreshToken);

        // Son giriş zamanını güncelle
        user.LastLoginAt = DateTime.UtcNow;
        _unitOfWork.Repository<User>().Update(user);

        await _unitOfWork.SaveChangesAsync();

        return ApiResponseDto<AuthResponseDto>.SuccessResponse(new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
            ExpiresAt = refreshToken.ExpiresAt,
            User = MapToUserDto(user)
        }, "Giriş başarılı");
    }

    /// <summary>
    /// Kullanıcı kaydı
    /// </summary>
    public async Task<ApiResponseDto<AuthResponseDto>> RegisterAsync(RegisterDto registerDto)
    {
        // E-posta kontrolü
        var existingUser = await _unitOfWork.Repository<User>()
            .ExistsAsync(u => u.Email == registerDto.Email);

        if (existingUser)
        {
            return ApiResponseDto<AuthResponseDto>.FailResponse("Bu e-posta adresi zaten kullanılıyor");
        }

        // Yeni kullanıcı oluştur
        var user = new User
        {
            Name = registerDto.Name,
            Email = registerDto.Email,
            PasswordHash = HashPassword(registerDto.Password),
            Role = UserRole.User,
            IsActive = true
        };

        await _unitOfWork.Repository<User>().AddAsync(user);

        // Token oluştur
        var accessToken = _tokenService.GenerateAccessToken(user);
        var refreshToken = _tokenService.GenerateRefreshToken(user.Id);

        await _unitOfWork.Repository<RefreshToken>().AddAsync(refreshToken);
        await _unitOfWork.SaveChangesAsync();

        return ApiResponseDto<AuthResponseDto>.SuccessResponse(new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
            ExpiresAt = refreshToken.ExpiresAt,
            User = MapToUserDto(user)
        }, "Kayıt başarılı");
    }

    /// <summary>
    /// Token yenileme
    /// </summary>
    public async Task<ApiResponseDto<AuthResponseDto>> RefreshTokenAsync(string accessToken, string refreshToken)
    {
        // Access token'dan user ID al
        var userId = _tokenService.GetUserIdFromToken(accessToken);
        if (userId == null)
        {
            return ApiResponseDto<AuthResponseDto>.FailResponse("Geçersiz token");
        }

        // Refresh token'ı bul
        var storedToken = await _unitOfWork.Repository<RefreshToken>()
            .Query()
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Token == refreshToken && r.UserId == userId);

        if (storedToken == null || !storedToken.IsActive)
        {
            return ApiResponseDto<AuthResponseDto>.FailResponse("Geçersiz veya süresi dolmuş refresh token");
        }

        var user = storedToken.User;

        if (!user.IsActive)
        {
            return ApiResponseDto<AuthResponseDto>.FailResponse("Hesabınız devre dışı bırakılmış");
        }

        // Eski refresh token'ı iptal et
        storedToken.IsRevoked = true;
        storedToken.RevokedAt = DateTime.UtcNow;

        // Yeni tokenlar oluştur
        var newAccessToken = _tokenService.GenerateAccessToken(user);
        var newRefreshToken = _tokenService.GenerateRefreshToken(user.Id);

        storedToken.ReplacedByToken = newRefreshToken.Token;

        await _unitOfWork.Repository<RefreshToken>().AddAsync(newRefreshToken);
        await _unitOfWork.SaveChangesAsync();

        return ApiResponseDto<AuthResponseDto>.SuccessResponse(new AuthResponseDto
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken.Token,
            ExpiresAt = newRefreshToken.ExpiresAt,
            User = MapToUserDto(user)
        }, "Token yenilendi");
    }

    /// <summary>
    /// Çıkış yapma
    /// </summary>
    public async Task<ApiResponseDto> LogoutAsync(Guid userId, string refreshToken)
    {
        var storedToken = await _unitOfWork.Repository<RefreshToken>()
            .GetAsync(r => r.Token == refreshToken && r.UserId == userId);

        if (storedToken != null && storedToken.IsActive)
        {
            storedToken.IsRevoked = true;
            storedToken.RevokedAt = DateTime.UtcNow;
            await _unitOfWork.SaveChangesAsync();
        }

        return ApiResponseDto.SuccessResponse("Çıkış başarılı");
    }

    /// <summary>
    /// Şifre değiştirme
    /// </summary>
    public async Task<ApiResponseDto> ChangePasswordAsync(Guid userId, ChangePasswordDto changePasswordDto)
    {
        var user = await _unitOfWork.Repository<User>().GetByIdAsync(userId);

        if (user == null)
        {
            return ApiResponseDto.FailResponse("Kullanıcı bulunamadı");
        }

        if (string.IsNullOrEmpty(user.PasswordHash))
        {
            return ApiResponseDto.FailResponse("Google hesapları için şifre değiştirilemez");
        }

        if (!VerifyPassword(changePasswordDto.CurrentPassword, user.PasswordHash))
        {
            return ApiResponseDto.FailResponse("Mevcut şifre hatalı");
        }

        user.PasswordHash = HashPassword(changePasswordDto.NewPassword);
        _unitOfWork.Repository<User>().Update(user);

        // Tüm refresh tokenları iptal et (güvenlik için)
        await RevokeAllUserTokens(userId);

        await _unitOfWork.SaveChangesAsync();

        return ApiResponseDto.SuccessResponse("Şifre başarıyla değiştirildi");
    }

    /// <summary>
    /// Kullanıcının tüm oturumlarını sonlandır
    /// </summary>
    public async Task<ApiResponseDto> RevokeAllTokensAsync(Guid userId)
    {
        await RevokeAllUserTokens(userId);
        await _unitOfWork.SaveChangesAsync();

        return ApiResponseDto.SuccessResponse("Tüm oturumlar sonlandırıldı");
    }

    /// <summary>
    /// Google ile giriş
    /// </summary>
    public async Task<ApiResponseDto<AuthResponseDto>> GoogleLoginAsync(GoogleLoginDto googleLoginDto)
    {
        try
        {
            // 1. Google ID token'ı doğrula
            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[] { _jwtSettings.GoogleClientId }
            };

            var payload = await GoogleJsonWebSignature.ValidateAsync(googleLoginDto.IdToken, settings);

            var email = payload.Email;
            var name = payload.Name;
            var googleId = payload.Subject;

            if (string.IsNullOrEmpty(email))
            {
                return ApiResponseDto<AuthResponseDto>.FailResponse("Google hesabında e-posta bulunamadı");
            }

            // 2. Kullanıcıyı email ile bul
            var user = await _unitOfWork.Repository<User>()
                .GetAsync(u => u.Email == email);

            // 3. Kullanıcı yoksa oluştur
            if (user == null)
            {
                user = new User
                {
                    Name = name ?? email.Split('@')[0],
                    Email = email,
                    PasswordHash = null,
                    GoogleId = googleId,
                    AuthProvider = "Google",
                    Role = UserRole.User,
                    IsActive = true
                };

                await _unitOfWork.Repository<User>().AddAsync(user);
                await _unitOfWork.SaveChangesAsync();
            }
            else
            {
                // Mevcut kullanıcıya Google ID bağla (ilk Google login)
                if (string.IsNullOrEmpty(user.GoogleId))
                {
                    user.GoogleId = googleId;
                    if (string.IsNullOrEmpty(user.AuthProvider))
                        user.AuthProvider = string.IsNullOrEmpty(user.PasswordHash) ? "Google" : "Local";
                }

                user.LastLoginAt = DateTime.UtcNow;
                _unitOfWork.Repository<User>().Update(user);
                await _unitOfWork.SaveChangesAsync();
            }

            // 4. Hesap aktif mi kontrol et
            if (!user.IsActive)
            {
                return ApiResponseDto<AuthResponseDto>.FailResponse("Hesabınız devre dışı bırakılmış");
            }

            // 5. JWT token oluştur (normal login ile aynı)
            var accessToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken(user.Id);

            await _unitOfWork.Repository<RefreshToken>().AddAsync(refreshToken);
            await _unitOfWork.SaveChangesAsync();

            return ApiResponseDto<AuthResponseDto>.SuccessResponse(new AuthResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                ExpiresAt = refreshToken.ExpiresAt,
                User = MapToUserDto(user)
            }, "Google ile giriş başarılı");
        }
        catch (InvalidJwtException)
        {
            return ApiResponseDto<AuthResponseDto>.FailResponse("Geçersiz Google token");
        }
    }

    #region Private Methods

    private async Task RevokeAllUserTokens(Guid userId)
    {
        var activeTokens = await _unitOfWork.Repository<RefreshToken>()
            .GetAllAsync(r => r.UserId == userId && !r.IsRevoked);

        foreach (var token in activeTokens)
        {
            token.IsRevoked = true;
            token.RevokedAt = DateTime.UtcNow;
        }
    }

    private static string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, BCrypt.Net.BCrypt.GenerateSalt(12));
    }

    private static bool VerifyPassword(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }

    private static UserDto MapToUserDto(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role,
            IsActive = user.IsActive,
            LastLoginAt = user.LastLoginAt,
            CreatedAt = user.CreatedAt
        };
    }

    #endregion
}
