using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleConditionTracker.Application.Dtos.Reports;

namespace VehicleConditionTracker.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/reports")]
public class ReportsController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<VehicleReportDto>), StatusCodes.Status200OK)]
    public IActionResult GetReports() => Ok(Array.Empty<VehicleReportDto>());

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(VehicleReportDto), StatusCodes.Status200OK)]
    public IActionResult GetReport(Guid id) => Ok();

    [HttpPost]
    [ProducesResponseType(typeof(VehicleReportDto), StatusCodes.Status201Created)]
    public IActionResult Create([FromBody] CreateVehicleReportRequest request)
    {
        // TODO: implement creation
        return CreatedAtAction(nameof(GetReport), new { id = Guid.NewGuid() }, null);
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, [FromBody] UpdateVehicleReportRequest request)
    {
        // TODO: implement update
        return NoContent();
    }

    [HttpPatch("{id:guid}/status")]
    public IActionResult UpdateStatus(Guid id, [FromBody] UpdateReportStatusRequest request)
    {
        // TODO: update status
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        // TODO: implement delete
        return NoContent();
    }

    [HttpGet("{id:guid}/pdf")]
    public IActionResult GetPdf(Guid id)
    {
        // TODO: generate pdf
        return File(Array.Empty<byte>(), "application/pdf", $"report-{id}.pdf");
    }
}
