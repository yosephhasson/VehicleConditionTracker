using Microsoft.AspNetCore.Mvc;
using VehicleConditionTracker.Application.Common.Interfaces;
using VehicleConditionTracker.Application.Dtos.Auth;

namespace VehicleConditionTracker.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthController(IJwtTokenGenerator jwtTokenGenerator)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    public IActionResult Register([FromBody] RegisterRequestDto request)
    {
        // TODO: implement persistence & hashing
        var token = _jwtTokenGenerator.GenerateToken(Guid.NewGuid(), request.Email);
        return Ok(new AuthResponseDto(request.Email, token));
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
    public IActionResult Login([FromBody] LoginRequestDto request)
    {
        // TODO: validate credentials
        var token = _jwtTokenGenerator.GenerateToken(Guid.NewGuid(), request.Email);
        return Ok(new AuthResponseDto(request.Email, token));
    }
}
