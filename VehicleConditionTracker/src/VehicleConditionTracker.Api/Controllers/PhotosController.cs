using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VehicleConditionTracker.Api.Controllers;

[ApiController]
[Authorize]
[Route("api")]
public class PhotosController : ControllerBase
{
    [HttpPost("reports/{reportId:guid}/photos")]
    public IActionResult UploadReportPhoto(Guid reportId, IFormFile file)
    {
        // TODO: save file
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPost("sections/{sectionId:guid}/photos")]
    public IActionResult UploadSectionPhoto(Guid sectionId, IFormFile file)
    {
        // TODO: save file
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpDelete("photos/{photoId:guid}")]
    public IActionResult DeletePhoto(Guid photoId)
    {
        // TODO: delete photo
        return NoContent();
    }
}
