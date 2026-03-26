using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleConditionTracker.Application.Common.Interfaces;
using VehicleConditionTracker.Application.Dtos.Reports;

namespace VehicleConditionTracker.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/reports")]
public class ReportsController : ControllerBase
{
    private readonly IReportService _reportService;

    public ReportsController(IReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<VehicleReportDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetReports()
    {
        var reports = await _reportService.GetAllAsync();
        return Ok(reports);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(VehicleReportDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetReport(Guid id)
    {
        var report = await _reportService.GetAsync(id);
        if (report is null) return NotFound();
        return Ok(report);
    }

    [HttpPost]
    [ProducesResponseType(typeof(VehicleReportDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateVehicleReportRequest request)
    {
        var created = await _reportService.CreateAsync(request);
        return CreatedAtAction(nameof(GetReport), new { id = created.Id }, created);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateVehicleReportRequest request)
    {
        var ok = await _reportService.UpdateAsync(id, request);
        return ok ? NoContent() : NotFound();
    }

    [HttpPatch("{id:guid}/status")]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] UpdateReportStatusRequest request)
    {
        var ok = await _reportService.UpdateStatusAsync(id, request.Status);
        return ok ? NoContent() : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var ok = await _reportService.DeleteAsync(id);
        return ok ? NoContent() : NotFound();
    }

    [HttpGet("{id:guid}/pdf")]
    public IActionResult GetPdf(Guid id)
    {
        // TODO: generate pdf
        return File(Array.Empty<byte>(), "application/pdf", $"report-{id}.pdf");
    }
}
