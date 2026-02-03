using Core.DTOs.Common;
using Core.DTOs.ListingDtos;
using Core.DTOs.VehicleDtos;

namespace BussinessLayer.Abstract;

/// <summary>
/// İlan yönetimi servisi
/// </summary>
public interface IListingService
{
    /// <summary>
    /// Araç ilanı oluştur (Listing + Vehicle birlikte)
    /// </summary>
    Task<ApiResponseDto<ListingDto>> CreateVehicleListingAsync(CreateListingDto listingDto, CreateVehicleDto vehicleDto);

    /// <summary>
    /// İlan getir (ID ile)
    /// </summary>
    Task<ApiResponseDto<ListingDto>> GetListingByIdAsync(Guid id);

    /// <summary>
    /// İlanları listele (filtreleme + sayfalama)
    /// </summary>
    Task<ApiResponseDto<PaginatedResponseDto<ListingDto>>> GetListingsAsync(ListingFilterDto filter);

    /// <summary>
    /// İlan güncelle
    /// </summary>
    Task<ApiResponseDto<ListingDto>> UpdateListingAsync(Guid id, UpdateListingDto updateDto);

    /// <summary>
    /// Araç bilgilerini güncelle
    /// </summary>
    Task<ApiResponseDto<VehicleDto>> UpdateVehicleAsync(Guid listingId, UpdateVehicleDto updateDto);

    /// <summary>
    /// İlan sil
    /// </summary>
    Task<ApiResponseDto> DeleteListingAsync(Guid id);
}
