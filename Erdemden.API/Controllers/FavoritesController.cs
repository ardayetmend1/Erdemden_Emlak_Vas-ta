using BussinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Erdemden.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FavoritesController : ControllerBase
{
    private readonly IFavoriteService _favoriteService;

    public FavoritesController(IFavoriteService favoriteService)
    {
        _favoriteService = favoriteService;
    }

    /// <summary>
    /// Kullanıcının favori ilan ID'lerini getir
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetFavorites()
    {
        var userId = GetUserId();
        if (userId == null)
        {
            return Unauthorized(new { success = false, message = "Kullanıcı kimliği bulunamadı." });
        }

        var result = await _favoriteService.GetUserFavoritesAsync(userId.Value);
        return Ok(result);
    }

    /// <summary>
    /// Favorilere ilan ekle
    /// </summary>
    [HttpPost("{listingId:guid}")]
    public async Task<IActionResult> AddFavorite(Guid listingId)
    {
        var userId = GetUserId();
        if (userId == null)
        {
            return Unauthorized(new { success = false, message = "Kullanıcı kimliği bulunamadı." });
        }

        var result = await _favoriteService.AddFavoriteAsync(userId.Value, listingId);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Favorilerden ilan çıkar
    /// </summary>
    [HttpDelete("{listingId:guid}")]
    public async Task<IActionResult> RemoveFavorite(Guid listingId)
    {
        var userId = GetUserId();
        if (userId == null)
        {
            return Unauthorized(new { success = false, message = "Kullanıcı kimliği bulunamadı." });
        }

        var result = await _favoriteService.RemoveFavoriteAsync(userId.Value, listingId);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// JWT'den kullanıcı ID'sini al
    /// </summary>
    private Guid? GetUserId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
        {
            return null;
        }

        return userId;
    }
}
