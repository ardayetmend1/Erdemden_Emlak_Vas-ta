using Core.DTOs.Common;
using DataAcessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Erdemden.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LookupsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public LookupsController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Markaları getir (opsiyonel: araç tipi ve kasa tipine göre filtrele)
    /// </summary>
    [HttpGet("brands")]
    public async Task<IActionResult> GetBrands([FromQuery] Guid? vehicleTypeId = null, [FromQuery] Guid? bodyTypeId = null)
    {
        if (vehicleTypeId.HasValue || bodyTypeId.HasValue)
        {
            var modelsQuery = _unitOfWork.Repository<Model>().Query();

            if (bodyTypeId.HasValue)
            {
                modelsQuery = modelsQuery.Where(m => m.BodyTypeId == bodyTypeId.Value);
            }
            else if (vehicleTypeId.HasValue)
            {
                modelsQuery = modelsQuery.Where(m => m.BodyType != null && m.BodyType.VehicleTypeId == vehicleTypeId.Value);
            }

            var brands = await modelsQuery
                .Select(m => m.Brand)
                .Distinct()
                .OrderBy(b => b.Name)
                .Select(b => new LookupDto { Id = b.Id, Name = b.Name })
                .ToListAsync();

            return Ok(ApiResponseDto<List<LookupDto>>.SuccessResponse(brands));
        }

        var allBrands = await _unitOfWork.Repository<Brand>()
            .Query()
            .OrderBy(b => b.Name)
            .Select(b => new LookupDto { Id = b.Id, Name = b.Name })
            .ToListAsync();

        return Ok(ApiResponseDto<List<LookupDto>>.SuccessResponse(allBrands));
    }

    /// <summary>
    /// Markaya ve opsiyonel olarak kasa tipine ait modelleri getir
    /// </summary>
    [HttpGet("models/{brandId:guid}")]
    public async Task<IActionResult> GetModelsByBrand(Guid brandId, [FromQuery] Guid? bodyTypeId = null)
    {
        var query = _unitOfWork.Repository<Model>()
            .Query()
            .Where(m => m.BrandId == brandId);

        // Eğer bodyTypeId verilmişse, o kasa tipine ait modelleri filtrele
        if (bodyTypeId.HasValue)
        {
            query = query.Where(m => m.BodyTypeId == bodyTypeId.Value);
        }

        var models = await query
            .OrderBy(m => m.Name)
            .Select(m => new LookupWithParentDto
            {
                Id = m.Id,
                Name = m.Name,
                ParentId = m.BrandId,
                ParentName = m.Brand.Name
            })
            .ToListAsync();

        return Ok(ApiResponseDto<List<LookupWithParentDto>>.SuccessResponse(models));
    }

    /// <summary>
    /// Tüm şehirleri getir
    /// </summary>
    [HttpGet("cities")]
    public async Task<IActionResult> GetCities()
    {
        var cities = await _unitOfWork.Repository<City>()
            .Query()
            .OrderBy(c => c.Name)
            .Select(c => new LookupDto { Id = c.Id, Name = c.Name })
            .ToListAsync();

        return Ok(ApiResponseDto<List<LookupDto>>.SuccessResponse(cities));
    }

    /// <summary>
    /// Şehre ait ilçeleri getir
    /// </summary>
    [HttpGet("districts/{cityId:guid}")]
    public async Task<IActionResult> GetDistrictsByCity(Guid cityId)
    {
        var districts = await _unitOfWork.Repository<District>()
            .Query()
            .Where(d => d.CityId == cityId)
            .OrderBy(d => d.Name)
            .Select(d => new LookupWithParentDto
            {
                Id = d.Id,
                Name = d.Name,
                ParentId = d.CityId,
                ParentName = d.City.Name
            })
            .ToListAsync();

        return Ok(ApiResponseDto<List<LookupWithParentDto>>.SuccessResponse(districts));
    }

    /// <summary>
    /// Araç tiplerini getir
    /// </summary>
    [HttpGet("vehicle-types")]
    public async Task<IActionResult> GetVehicleTypes()
    {
        var vehicleTypes = await _unitOfWork.Repository<VehicleType>()
            .Query()
            .OrderBy(v => v.Name)
            .Select(v => new LookupDto { Id = v.Id, Name = v.Name })
            .ToListAsync();

        return Ok(ApiResponseDto<List<LookupDto>>.SuccessResponse(vehicleTypes));
    }

    /// <summary>
    /// Yakıt tiplerini getir
    /// </summary>
    [HttpGet("fuel-types")]
    public async Task<IActionResult> GetFuelTypes()
    {
        var fuelTypes = await _unitOfWork.Repository<FuelType>()
            .Query()
            .OrderBy(f => f.Name)
            .Select(f => new LookupDto { Id = f.Id, Name = f.Name })
            .ToListAsync();

        return Ok(ApiResponseDto<List<LookupDto>>.SuccessResponse(fuelTypes));
    }

    /// <summary>
    /// Vites tiplerini getir
    /// </summary>
    [HttpGet("transmission-types")]
    public async Task<IActionResult> GetTransmissionTypes()
    {
        var transmissionTypes = await _unitOfWork.Repository<TransmissionType>()
            .Query()
            .OrderBy(t => t.Name)
            .Select(t => new LookupDto { Id = t.Id, Name = t.Name })
            .ToListAsync();

        return Ok(ApiResponseDto<List<LookupDto>>.SuccessResponse(transmissionTypes));
    }

    /// <summary>
    /// Kasa tiplerini getir (araç tipine göre)
    /// </summary>
    [HttpGet("body-types")]
    public async Task<IActionResult> GetBodyTypes([FromQuery] Guid? vehicleTypeId = null)
    {
        var query = _unitOfWork.Repository<BodyType>().Query();

        if (vehicleTypeId.HasValue)
        {
            query = query.Where(b => b.VehicleTypeId == vehicleTypeId.Value);
        }

        var bodyTypes = await query
            .OrderBy(b => b.Name)
            .Select(b => new LookupWithParentDto
            {
                Id = b.Id,
                Name = b.Name,
                ParentId = b.VehicleTypeId,
                ParentName = b.VehicleType.Name
            })
            .ToListAsync();

        return Ok(ApiResponseDto<List<LookupWithParentDto>>.SuccessResponse(bodyTypes));
    }

    /// <summary>
    /// Modele ait paketleri getir
    /// </summary>
    [HttpGet("packages/{modelId:guid}")]
    public async Task<IActionResult> GetPackagesByModel(Guid modelId)
    {
        var packages = await _unitOfWork.Repository<Package>()
            .Query()
            .Where(p => p.ModelId == modelId)
            .OrderBy(p => p.Name)
            .Select(p => new LookupWithParentDto
            {
                Id = p.Id,
                Name = p.Name,
                ParentId = p.ModelId,
                ParentName = p.Model.Name
            })
            .ToListAsync();

        return Ok(ApiResponseDto<List<LookupWithParentDto>>.SuccessResponse(packages));
    }

    /// <summary>
    /// Emlak tiplerini getir
    /// </summary>
    [HttpGet("housing-types")]
    public async Task<IActionResult> GetHousingTypes()
    {
        var housingTypes = await _unitOfWork.Repository<HousingType>()
            .Query()
            .OrderBy(h => h.Name)
            .Select(h => new LookupDto { Id = h.Id, Name = h.Name })
            .ToListAsync();

        return Ok(ApiResponseDto<List<LookupDto>>.SuccessResponse(housingTypes));
    }

    /// <summary>
    /// Veritabanı durumunu teşhis et - BodyType ve Model ilişkilerini kontrol et
    /// </summary>
    [HttpGet("diagnostic")]
    public async Task<IActionResult> GetDiagnostic()
    {
        var bodyTypes = await _unitOfWork.Repository<BodyType>()
            .Query()
            .Select(b => new { b.Id, b.Name, b.VehicleTypeId })
            .ToListAsync();

        var modelStats = await _unitOfWork.Repository<Model>()
            .Query()
            .GroupBy(m => m.BodyTypeId)
            .Select(g => new { BodyTypeId = g.Key, Count = g.Count() })
            .ToListAsync();

        var sampleModels = await _unitOfWork.Repository<Model>()
            .Query()
            .OrderBy(m => m.Name)
            .Take(50)
            .Select(m => new { m.Id, m.Name, m.BodyTypeId, BrandName = m.Brand.Name })
            .ToListAsync();

        return Ok(new
        {
            bodyTypes,
            modelStats,
            sampleModels,
            totalModels = await _unitOfWork.Repository<Model>().Query().CountAsync(),
            modelsWithNullBodyType = await _unitOfWork.Repository<Model>().Query().CountAsync(m => m.BodyTypeId == null)
        });
    }

    /// <summary>
    /// Tüm lookup verilerini tek seferde getir (form için)
    /// </summary>
    [HttpGet("all")]
    public async Task<IActionResult> GetAllLookups()
    {
        var brands = await _unitOfWork.Repository<Brand>()
            .Query().OrderBy(b => b.Name)
            .Select(b => new LookupDto { Id = b.Id, Name = b.Name }).ToListAsync();

        var cities = await _unitOfWork.Repository<City>()
            .Query().OrderBy(c => c.Name)
            .Select(c => new LookupDto { Id = c.Id, Name = c.Name }).ToListAsync();

        var vehicleTypes = await _unitOfWork.Repository<VehicleType>()
            .Query().OrderBy(v => v.Name)
            .Select(v => new LookupDto { Id = v.Id, Name = v.Name }).ToListAsync();

        var fuelTypes = await _unitOfWork.Repository<FuelType>()
            .Query().OrderBy(f => f.Name)
            .Select(f => new LookupDto { Id = f.Id, Name = f.Name }).ToListAsync();

        var transmissionTypes = await _unitOfWork.Repository<TransmissionType>()
            .Query().OrderBy(t => t.Name)
            .Select(t => new LookupDto { Id = t.Id, Name = t.Name }).ToListAsync();

        var housingTypes = await _unitOfWork.Repository<HousingType>()
            .Query().OrderBy(h => h.Name)
            .Select(h => new LookupDto { Id = h.Id, Name = h.Name }).ToListAsync();

        var result = new
        {
            Brands = brands,
            Cities = cities,
            VehicleTypes = vehicleTypes,
            FuelTypes = fuelTypes,
            TransmissionTypes = transmissionTypes,
            HousingTypes = housingTypes
        };

        return Ok(ApiResponseDto<object>.SuccessResponse(result));
    }
}
