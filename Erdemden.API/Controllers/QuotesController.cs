using BussinessLayer.Abstract;
using Core.DTOs.QuoteRequestDtos;
using DataAcessLayer.Abstract;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Erdemden.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuotesController : ControllerBase
{
    private readonly IQuoteService _quoteService;
    private readonly IUnitOfWork _unitOfWork;

    public QuotesController(IQuoteService quoteService, IUnitOfWork unitOfWork)
    {
        _quoteService = quoteService;
        _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Yeni teklif talebi oluştur (Herkes erişebilir)
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateQuote([FromBody] CreateQuoteWithFilesDto request)
    {
        var expertReports = request.ExpertReports?.Select(r => new FileUploadDto
        {
            FileName = r.Name,
            ContentType = r.Type,
            Base64Data = r.Data.Contains(",") ? r.Data.Split(',')[1] : r.Data // Base64 data URI formatını handle et
        }).ToList();

        var result = await _quoteService.CreateQuoteAsync(request.Quote, expertReports);

        if (!result.Success)
        {
            return BadRequest(result);
        }

        return CreatedAtAction(nameof(GetQuoteById), new { id = result.Data!.Id }, result);
    }

    /// <summary>
    /// Tüm teklif taleplerini getir (Sadece Admin)
    /// </summary>
    [Authorize(Policy = "AdminOnly")]
    [HttpGet]
    public async Task<IActionResult> GetAllQuotes()
    {
        var result = await _quoteService.GetAllQuotesAsync();
        return Ok(result);
    }

    /// <summary>
    /// Kullanıcının kendi teklif taleplerini getir
    /// </summary>
    [Authorize]
    [HttpGet("my")]
    public async Task<IActionResult> GetMyQuotes()
    {
        var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;

        if (string.IsNullOrEmpty(email))
        {
            return BadRequest(new { success = false, message = "E-posta bilgisi bulunamadı." });
        }

        var result = await _quoteService.GetQuotesByEmailAsync(email);
        return Ok(result);
    }

    /// <summary>
    /// Teklif talebini ID ile getir (Sadece Admin)
    /// </summary>
    [Authorize(Policy = "AdminOnly")]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetQuoteById(Guid id)
    {
        var result = await _quoteService.GetQuoteByIdAsync(id);

        if (!result.Success)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Teklif talebini okundu olarak işaretle (Sadece Admin)
    /// </summary>
    [Authorize(Policy = "AdminOnly")]
    [HttpPut("{id:guid}/read")]
    public async Task<IActionResult> MarkAsRead(Guid id)
    {
        var result = await _quoteService.MarkAsReadAsync(id);

        if (!result.Success)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Teklif talebini sil (Sadece Admin)
    /// </summary>
    [Authorize(Policy = "AdminOnly")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteQuote(Guid id)
    {
        var result = await _quoteService.DeleteQuoteAsync(id);

        if (!result.Success)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    /// <summary>
    /// Ekspertiz raporu indir
    /// </summary>
    [HttpGet("reports/{reportId:guid}/download")]
    public async Task<IActionResult> DownloadExpertReport(Guid reportId)
    {
        var report = await _unitOfWork.Repository<ExpertReport>().GetByIdAsync(reportId);

        if (report == null || report.Data == null)
        {
            return NotFound(new { success = false, message = "Rapor bulunamadı." });
        }

        return File(report.Data, report.ContentType ?? "application/octet-stream", report.Name);
    }
}

/// <summary>
/// Teklif oluşturma isteği (dosyalarla birlikte)
/// </summary>
public class CreateQuoteWithFilesDto
{
    public CreateQuoteRequestDto Quote { get; set; } = new();
    public List<ExpertReportUploadDto>? ExpertReports { get; set; }
}

/// <summary>
/// Ekspertiz raporu yükleme DTO
/// </summary>
public class ExpertReportUploadDto
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Data { get; set; } = string.Empty; // Base64 encoded
}
