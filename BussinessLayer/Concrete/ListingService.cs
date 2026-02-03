using BussinessLayer.Abstract;
using Core.DTOs.Common;
using Core.DTOs.ListingDtos;
using Core.DTOs.VehicleDtos;
using Core.DTOs.ImageDtos;
using DataAcessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BussinessLayer.Concrete;

/// <summary>
/// İlan yönetimi servisi implementasyonu
/// </summary>
public class ListingService : IListingService
{
    private readonly IUnitOfWork _unitOfWork;

    public ListingService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Araç ilanı oluştur (Listing + Vehicle birlikte)
    /// </summary>
    public async Task<ApiResponseDto<ListingDto>> CreateVehicleListingAsync(CreateListingDto listingDto, CreateVehicleDto vehicleDto)
    {
        try
        {
            await _unitOfWork.BeginTransactionAsync();

            // Listing oluştur
            var listing = new Listing
            {
                Title = listingDto.Title,
                Price = listingDto.Price,
                Currency = listingDto.Currency,
                Description = listingDto.Description,
                Category = ListingCategory.Vehicle,
                Status = listingDto.Status,
                CityId = listingDto.CityId,
                DistrictId = listingDto.DistrictId,
                PurchasePrice = listingDto.PurchasePrice,
                Expenses = listingDto.Expenses,
                ListingDate = DateTime.UtcNow
            };

            await _unitOfWork.Repository<Listing>().AddAsync(listing);
            await _unitOfWork.SaveChangesAsync();

            // Vehicle oluştur (Listing ID'yi kullanarak)
            var vehicle = new Vehicle
            {
                ListingId = listing.Id,
                Year = vehicleDto.Year,
                Km = vehicleDto.Km,
                Color = vehicleDto.Color,
                DamageStatus = vehicleDto.DamageStatus,
                VehicleTypeId = vehicleDto.VehicleTypeId,
                BrandId = vehicleDto.BrandId,
                ModelId = vehicleDto.ModelId,
                FuelTypeId = vehicleDto.FuelTypeId,
                TransmissionTypeId = vehicleDto.TransmissionTypeId,
                BodyTypeId = vehicleDto.BodyTypeId
            };

            await _unitOfWork.Repository<Vehicle>().AddAsync(vehicle);
            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork.CommitTransactionAsync();

            // Oluşturulan ilanı getir
            return await GetListingByIdAsync(listing.Id);
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return ApiResponseDto<ListingDto>.FailResponse($"İlan oluşturulurken hata: {ex.Message}");
        }
    }

    /// <summary>
    /// İlan getir (ID ile)
    /// </summary>
    public async Task<ApiResponseDto<ListingDto>> GetListingByIdAsync(Guid id)
    {
        var listing = await _unitOfWork.Repository<Listing>()
            .Query()
            .Include(l => l.City)
            .Include(l => l.District)
            .Include(l => l.Images)
            .Include(l => l.Vehicle)
                .ThenInclude(v => v!.VehicleType)
            .Include(l => l.Vehicle)
                .ThenInclude(v => v!.Brand)
            .Include(l => l.Vehicle)
                .ThenInclude(v => v!.Model)
            .Include(l => l.Vehicle)
                .ThenInclude(v => v!.FuelType)
            .Include(l => l.Vehicle)
                .ThenInclude(v => v!.TransmissionType)
            .Include(l => l.Vehicle)
                .ThenInclude(v => v!.BodyType)
            .FirstOrDefaultAsync(l => l.Id == id);

        if (listing == null)
        {
            return ApiResponseDto<ListingDto>.FailResponse("İlan bulunamadı");
        }

        return ApiResponseDto<ListingDto>.SuccessResponse(MapToListingDto(listing));
    }

    /// <summary>
    /// İlanları listele (filtreleme + sayfalama)
    /// </summary>
    public async Task<ApiResponseDto<PaginatedResponseDto<ListingDto>>> GetListingsAsync(ListingFilterDto filter)
    {
        var query = _unitOfWork.Repository<Listing>()
            .Query()
            .Include(l => l.City)
            .Include(l => l.District)
            .Include(l => l.Images)
            .Include(l => l.Vehicle)
                .ThenInclude(v => v!.VehicleType)
            .Include(l => l.Vehicle)
                .ThenInclude(v => v!.Brand)
            .Include(l => l.Vehicle)
                .ThenInclude(v => v!.Model)
            .Include(l => l.Vehicle)
                .ThenInclude(v => v!.FuelType)
            .Include(l => l.Vehicle)
                .ThenInclude(v => v!.TransmissionType)
            .Include(l => l.Vehicle)
                .ThenInclude(v => v!.BodyType)
            .AsQueryable();

        // Filtreleme
        if (filter.Category.HasValue)
            query = query.Where(l => l.Category == filter.Category.Value);

        if (filter.Status.HasValue)
            query = query.Where(l => l.Status == filter.Status.Value);

        if (filter.MinPrice.HasValue)
            query = query.Where(l => l.Price >= filter.MinPrice.Value);

        if (filter.MaxPrice.HasValue)
            query = query.Where(l => l.Price <= filter.MaxPrice.Value);

        if (filter.CityId.HasValue)
            query = query.Where(l => l.CityId == filter.CityId.Value);

        if (filter.DistrictId.HasValue)
            query = query.Where(l => l.DistrictId == filter.DistrictId.Value);

        if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            query = query.Where(l => l.Title.Contains(filter.SearchTerm) ||
                                     (l.Description != null && l.Description.Contains(filter.SearchTerm)));

        if (filter.FromDate.HasValue)
            query = query.Where(l => l.ListingDate >= filter.FromDate.Value);

        if (filter.ToDate.HasValue)
            query = query.Where(l => l.ListingDate <= filter.ToDate.Value);

        // Toplam kayıt sayısı
        var totalCount = await query.CountAsync();

        // Sıralama ve sayfalama
        var listings = await query
            .OrderByDescending(l => l.CreatedAt)
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToListAsync();

        var listingDtos = listings.Select(MapToListingDto).ToList();

        var result = new PaginatedResponseDto<ListingDto>
        {
            Items = listingDtos,
            TotalCount = totalCount,
            Page = filter.PageNumber,
            PageSize = filter.PageSize,
            TotalPages = (int)Math.Ceiling(totalCount / (double)filter.PageSize)
        };

        return ApiResponseDto<PaginatedResponseDto<ListingDto>>.SuccessResponse(result);
    }

    /// <summary>
    /// İlan güncelle
    /// </summary>
    public async Task<ApiResponseDto<ListingDto>> UpdateListingAsync(Guid id, UpdateListingDto updateDto)
    {
        var listing = await _unitOfWork.Repository<Listing>().GetByIdAsync(id);

        if (listing == null)
        {
            return ApiResponseDto<ListingDto>.FailResponse("İlan bulunamadı");
        }

        // Güncelleme
        if (updateDto.Title != null) listing.Title = updateDto.Title;
        if (updateDto.Price.HasValue) listing.Price = updateDto.Price.Value;
        if (updateDto.Currency != null) listing.Currency = updateDto.Currency;
        if (updateDto.Description != null) listing.Description = updateDto.Description;
        if (updateDto.Status.HasValue) listing.Status = updateDto.Status.Value;
        if (updateDto.CityId.HasValue) listing.CityId = updateDto.CityId.Value;
        if (updateDto.DistrictId.HasValue) listing.DistrictId = updateDto.DistrictId;

        // Satış bilgileri
        if (updateDto.PurchasePrice.HasValue) listing.PurchasePrice = updateDto.PurchasePrice;
        if (updateDto.Expenses.HasValue) listing.Expenses = updateDto.Expenses;
        if (updateDto.SalePrice.HasValue) listing.SalePrice = updateDto.SalePrice;
        if (updateDto.SoldDate.HasValue) listing.SoldDate = updateDto.SoldDate;
        if (updateDto.SoldTo != null) listing.SoldTo = updateDto.SoldTo;
        if (updateDto.SoldToPhone != null) listing.SoldToPhone = updateDto.SoldToPhone;
        if (updateDto.SoldToEmail != null) listing.SoldToEmail = updateDto.SoldToEmail;
        if (updateDto.BuyerReason.HasValue) listing.BuyerReason = updateDto.BuyerReason;

        _unitOfWork.Repository<Listing>().Update(listing);
        await _unitOfWork.SaveChangesAsync();

        return await GetListingByIdAsync(id);
    }

    /// <summary>
    /// Araç bilgilerini güncelle
    /// </summary>
    public async Task<ApiResponseDto<VehicleDto>> UpdateVehicleAsync(Guid listingId, UpdateVehicleDto updateDto)
    {
        var vehicle = await _unitOfWork.Repository<Vehicle>()
            .Query()
            .Include(v => v.VehicleType)
            .Include(v => v.Brand)
            .Include(v => v.Model)
            .Include(v => v.FuelType)
            .Include(v => v.TransmissionType)
            .Include(v => v.BodyType)
            .FirstOrDefaultAsync(v => v.ListingId == listingId);

        if (vehicle == null)
        {
            return ApiResponseDto<VehicleDto>.FailResponse("Araç bulunamadı");
        }

        // Güncelleme
        if (updateDto.Year.HasValue) vehicle.Year = updateDto.Year.Value;
        if (updateDto.Km.HasValue) vehicle.Km = updateDto.Km.Value;
        if (updateDto.Color != null) vehicle.Color = updateDto.Color;
        if (updateDto.DamageStatus != null) vehicle.DamageStatus = updateDto.DamageStatus;
        if (updateDto.VehicleTypeId.HasValue) vehicle.VehicleTypeId = updateDto.VehicleTypeId.Value;
        if (updateDto.BrandId.HasValue) vehicle.BrandId = updateDto.BrandId.Value;
        if (updateDto.ModelId.HasValue) vehicle.ModelId = updateDto.ModelId.Value;
        if (updateDto.FuelTypeId.HasValue) vehicle.FuelTypeId = updateDto.FuelTypeId.Value;
        if (updateDto.TransmissionTypeId.HasValue) vehicle.TransmissionTypeId = updateDto.TransmissionTypeId.Value;
        if (updateDto.BodyTypeId.HasValue) vehicle.BodyTypeId = updateDto.BodyTypeId;

        _unitOfWork.Repository<Vehicle>().Update(vehicle);
        await _unitOfWork.SaveChangesAsync();

        // Güncel vehicle'ı tekrar getir
        vehicle = await _unitOfWork.Repository<Vehicle>()
            .Query()
            .Include(v => v.VehicleType)
            .Include(v => v.Brand)
            .Include(v => v.Model)
            .Include(v => v.FuelType)
            .Include(v => v.TransmissionType)
            .Include(v => v.BodyType)
            .FirstOrDefaultAsync(v => v.ListingId == listingId);

        return ApiResponseDto<VehicleDto>.SuccessResponse(MapToVehicleDto(vehicle!));
    }

    /// <summary>
    /// İlan sil
    /// </summary>
    public async Task<ApiResponseDto> DeleteListingAsync(Guid id)
    {
        var listing = await _unitOfWork.Repository<Listing>()
            .Query()
            .Include(l => l.Vehicle)
            .Include(l => l.Images)
            .FirstOrDefaultAsync(l => l.Id == id);

        if (listing == null)
        {
            return ApiResponseDto.FailResponse("İlan bulunamadı");
        }

        _unitOfWork.Repository<Listing>().Delete(listing);
        await _unitOfWork.SaveChangesAsync();

        return ApiResponseDto.SuccessResponse("İlan başarıyla silindi");
    }

    #region Private Methods

    private static ListingDto MapToListingDto(Listing listing)
    {
        return new ListingDto
        {
            Id = listing.Id,
            Title = listing.Title,
            Price = listing.Price,
            Currency = listing.Currency,
            ImageUrl = listing.Images?.FirstOrDefault()?.ImageUrl,
            Description = listing.Description,
            Category = listing.Category,
            Status = listing.Status,
            ListingDate = listing.ListingDate,
            CreatedAt = listing.CreatedAt,
            UpdatedAt = listing.UpdatedAt,
            City = listing.City != null ? new LookupDto { Id = listing.City.Id, Name = listing.City.Name } : null,
            District = listing.District != null ? new LookupWithParentDto
            {
                Id = listing.District.Id,
                Name = listing.District.Name,
                ParentId = listing.District.CityId,
                ParentName = listing.City?.Name
            } : null,
            Images = listing.Images?.Select(i => new ImageDto
            {
                Id = i.Id,
                ImageUrl = i.ImageUrl,
                IsCover = i.IsCover,
                Order = i.Order
            }).ToList() ?? new List<ImageDto>(),
            Vehicle = listing.Vehicle != null ? MapToVehicleDto(listing.Vehicle) : null,
            SaleInfo = new ListingSaleInfoDto
            {
                PurchasePrice = listing.PurchasePrice,
                Expenses = listing.Expenses,
                SalePrice = listing.SalePrice,
                SoldDate = listing.SoldDate,
                SoldTo = listing.SoldTo,
                SoldToPhone = listing.SoldToPhone,
                SoldToEmail = listing.SoldToEmail,
                BuyerReason = listing.BuyerReason
            }
        };
    }

    private static VehicleDto MapToVehicleDto(Vehicle vehicle)
    {
        return new VehicleDto
        {
            Year = vehicle.Year ?? 0,
            Km = vehicle.Km ?? 0,
            Color = vehicle.Color,
            DamageStatus = vehicle.DamageStatus,
            VehicleType = new LookupDto { Id = vehicle.VehicleType.Id, Name = vehicle.VehicleType.Name },
            Brand = new LookupDto { Id = vehicle.Brand.Id, Name = vehicle.Brand.Name },
            Model = new LookupWithParentDto
            {
                Id = vehicle.Model.Id,
                Name = vehicle.Model.Name,
                ParentId = vehicle.Model.BrandId,
                ParentName = vehicle.Brand?.Name ?? string.Empty
            },
            FuelType = new LookupDto { Id = vehicle.FuelType.Id, Name = vehicle.FuelType.Name },
            TransmissionType = new LookupDto { Id = vehicle.TransmissionType.Id, Name = vehicle.TransmissionType.Name },
            BodyType = vehicle.BodyType != null ? new LookupWithParentDto
            {
                Id = vehicle.BodyType.Id,
                Name = vehicle.BodyType.Name,
                ParentId = vehicle.BodyType.VehicleTypeId,
                ParentName = vehicle.VehicleType?.Name ?? string.Empty
            } : null
        };
    }

    #endregion
}
