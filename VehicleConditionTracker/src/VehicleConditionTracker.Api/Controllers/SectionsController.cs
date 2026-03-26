using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleConditionTracker.Application.Dtos.Sections;

namespace VehicleConditionTracker.Api.Controllers;

[ApiController]
[Authorize]
[Route("api")]
public class SectionsController : ControllerBase
{
    [HttpPost("reports/{reportId:guid}/sections")]
    public IActionResult CreateSection(Guid reportId, [FromBody] CreateSectionRequest request)
    {
        // TODO: add section to report
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("sections/{sectionId:guid}")]
    public IActionResult UpdateSection(Guid sectionId, [FromBody] UpdateSectionRequest request)
    {
        // TODO: update section
        return NoContent();
    }

    [HttpDelete("sections/{sectionId:guid}")]
    public IActionResult DeleteSection(Guid sectionId)
    {
        // TODO: delete section
        return NoContent();
    }
}
