using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleConditionTracker.Application.Common.Interfaces;
using VehicleConditionTracker.Application.Dtos.Sections;

namespace VehicleConditionTracker.Api.Controllers;

[ApiController]
[Authorize]
[Route("api")]
public class SectionsController : ControllerBase
{
    private readonly ISectionService _sectionService;

    public SectionsController(ISectionService sectionService)
    {
        _sectionService = sectionService;
    }

    [HttpPost("reports/{reportId:guid}/sections")]
    public async Task<IActionResult> CreateSection(Guid reportId, [FromBody] CreateSectionRequest request)
    {
        var ok = await _sectionService.CreateAsync(reportId, request);
        return ok ? StatusCode(StatusCodes.Status201Created) : NotFound();
    }

    [HttpPut("sections/{sectionId:guid}")]
    public async Task<IActionResult> UpdateSection(Guid sectionId, [FromBody] UpdateSectionRequest request)
    {
        var ok = await _sectionService.UpdateAsync(sectionId, request);
        return ok ? NoContent() : NotFound();
    }

    [HttpDelete("sections/{sectionId:guid}")]
    public async Task<IActionResult> DeleteSection(Guid sectionId)
    {
        var ok = await _sectionService.DeleteAsync(sectionId);
        return ok ? NoContent() : NotFound();
    }
}
