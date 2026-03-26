using System.Security.Claims;
using VehicleConditionTracker.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;

namespace VehicleConditionTracker.Infrastructure.Authentication;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? UserId
    {
        get
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)
                        ?? _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.Name)
                        ?? _httpContextAccessor.HttpContext?.User?.FindFirstValue(JwtRegisteredClaimNames.Sub);

            return Guid.TryParse(userId, out var id) ? id : null;
        }
    }
}
