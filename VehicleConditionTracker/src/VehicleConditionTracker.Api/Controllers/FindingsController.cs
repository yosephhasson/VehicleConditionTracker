using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleConditionTracker.Application.Dtos.Findings;

namespace VehicleConditionTracker.Api.Controllers;

[ApiController]
[Authorize]
[Route("api")]
public class FindingsController : ControllerBase
{
    [HttpPost("sections/{sectionId:guid}/findings")]
    public IActionResult CreateFinding(Guid sectionId, [FromBody] CreateFindingRequest request)
    {
        // TODO: add finding
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("findings/{findingId:guid}")]
    public IActionResult UpdateFinding(Guid findingId, [FromBody] UpdateFindingRequest request)
    {
        // TODO: update finding
        return NoContent();
    }

    [HttpDelete("findings/{findingId:guid}")]
    public IActionResult DeleteFinding(Guid findingId)
    {
        // TODO: delete finding
        return NoContent();
    }
}
