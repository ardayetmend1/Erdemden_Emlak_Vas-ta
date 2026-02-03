using BussinessLayer.Abstract;
using Core.DTOs.Common;
using Core.DTOs.ListingDtos;
using Core.DTOs.VehicleDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Erdemden.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ListingsController : ControllerBase
{
    private readonly IListingService _listingService;

    public ListingsController(IListingService listingService)
    {
        _listingService = listingService;
    }

    /// <summary>
    /// Araç ilanı oluştur
    /// </summary>
    [Authorize(Policy = "AdminOnly")]
    [HttpPost("vehicle")]
    public async Task<IActionResult> CreateVehicleListing([FromBody] CreateVehicleListingRequest request)
    {
        var result = await _listingService.CreateVehicleListingAsync(request.Listing, request.Vehicle);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return CreatedAtAction(nameof(GetListingById), new { id = result.Data!.Id }, result);
    }

    /// <summary>
    /// İlanları listele (filtreleme + sayfalama)
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetListings([FromQuery] ListingFilterDto filter)
    {
        var result = await _listingService.GetListingsAsync(filter);
        return Ok(result);
    }

    /// <summary>
    /// İlan detayı getir
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetListingById(Guid id)
    {
        var result = await _listingService.GetListingByIdAsync(id);

        if (!result.Success)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// İlan güncelle
    /// </summary>
    [Authorize(Policy = "AdminOnly")]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateListing(Guid id, [FromBody] UpdateListingDto updateDto)
    {
        var result = await _listingService.UpdateListingAsync(id, updateDto);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Araç bilgilerini güncelle
    /// </summary>
    [Authorize(Policy = "AdminOnly")]
    [HttpPut("{id:guid}/vehicle")]
    public async Task<IActionResult> UpdateVehicle(Guid id, [FromBody] UpdateVehicleDto updateDto)
    {
        var result = await _listingService.UpdateVehicleAsync(id, updateDto);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// İlan sil
    /// </summary>
    [Authorize(Policy = "AdminOnly")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteListing(Guid id)
    {
        var result = await _listingService.DeleteListingAsync(id);

        if (!result.Success)
        {
            return NotFound(result);
        }

        return Ok(result);
    }
}

/// <summary>
/// Araç ilanı oluşturma isteği (Listing + Vehicle birlikte)
/// </summary>
public class CreateVehicleListingRequest
{
    public CreateListingDto Listing { get; set; } = null!;
    public CreateVehicleDto Vehicle { get; set; } = null!;
}
