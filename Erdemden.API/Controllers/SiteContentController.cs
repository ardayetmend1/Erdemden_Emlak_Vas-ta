using BussinessLayer.Abstract;
using Core.DTOs.SiteContentDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Erdemden.API.Controllers;

[ApiController]
[Route("api/site-content")]
public class SiteContentController : ControllerBase
{
    private readonly ISiteContentService _siteContentService;

    public SiteContentController(ISiteContentService siteContentService)
    {
        _siteContentService = siteContentService;
    }

    /// <summary>
    /// Tüm site içeriklerini getir (Public)
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAllContent()
    {
        var result = await _siteContentService.GetAllContentAsync();
        return Ok(result);
    }

    /// <summary>
    /// Belirli bir key'e ait içeriği getir (Public)
    /// </summary>
    [HttpGet("{key}")]
    public async Task<IActionResult> GetContentByKey(string key)
    {
        var result = await _siteContentService.GetContentByKeyAsync(key);

        if (!result.Success)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// İçerik oluştur veya güncelle (Admin Only)
    /// </summary>
    [Authorize(Policy = "AdminOnly")]
    [HttpPut("{key}")]
    public async Task<IActionResult> UpsertContent(string key, [FromBody] UpdateSiteContentDto dto)
    {
        var result = await _siteContentService.UpsertContentAsync(key, dto);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// İçeriği sil (Admin Only)
    /// </summary>
    [Authorize(Policy = "AdminOnly")]
    [HttpDelete("{key}")]
    public async Task<IActionResult> DeleteContent(string key)
    {
        var result = await _siteContentService.DeleteContentAsync(key);

        if (!result.Success)
        {
            return NotFound(result);
        }

        return Ok(result);
    }
}
