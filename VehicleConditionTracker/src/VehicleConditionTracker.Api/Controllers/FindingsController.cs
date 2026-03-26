using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleConditionTracker.Application.Common.Interfaces;
using VehicleConditionTracker.Application.Dtos.Findings;

namespace VehicleConditionTracker.Api.Controllers;

[ApiController]
[Authorize]
[Route("api")]
public class FindingsController : ControllerBase
{
    private readonly IFindingService _findingService;

    public FindingsController(IFindingService findingService)
    {
        _findingService = findingService;
    }

    [HttpPost("sections/{sectionId:guid}/findings")]
    public async Task<IActionResult> CreateFinding(Guid sectionId, [FromBody] CreateFindingRequest request)
    {
        var ok = await _findingService.CreateAsync(sectionId, request);
        return ok ? StatusCode(StatusCodes.Status201Created) : NotFound();
    }

    [HttpPut("findings/{findingId:guid}")]
    public async Task<IActionResult> UpdateFinding(Guid findingId, [FromBody] UpdateFindingRequest request)
    {
        var ok = await _findingService.UpdateAsync(findingId, request);
        return ok ? NoContent() : NotFound();
    }

    [HttpDelete("findings/{findingId:guid}")]
    public async Task<IActionResult> DeleteFinding(Guid findingId)
    {
        var ok = await _findingService.DeleteAsync(findingId);
        return ok ? NoContent() : NotFound();
    }
}
