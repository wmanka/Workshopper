using Microsoft.AspNetCore.Http;
using Workshopper.Application.Common.Abstractions;
using Workshopper.Application.Common.Models;
using Workshopper.Domain.Users.UserProfiles;
using Workshopper.Infrastructure.Authentication;

namespace Workshopper.Api.Sessions.Common;

public class CurrentUserProvider : ICurrentUserProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public CurrentUser? GetCurrentUser()
    {
        if (_httpContextAccessor.HttpContext?.User is null)
        {
            return null;
        }

        if (Guid.TryParse(GetClaimValue(CustomClaimType.UserId), out var userId))
        {
            return null;
        }

        Guid.TryParse(GetClaimValue(CustomClaimType.ProfileId), out var profileId);
        ProfileType.TryFromName(GetClaimValue(CustomClaimType.ProfileType), out var profileType);

        return new CurrentUser(
            userId: userId,
            profileId: profileId,
            profileType: profileType,
            roles: GetRoles());
    }

    private List<string> GetRoles()
    {
        return _httpContextAccessor.HttpContext!.User.Claims
            .Where(claim => claim.Type == CustomClaimType.Role)
            .Select(claim => claim.Value)
            .ToList();
    }

    private string? GetClaimValue(string claimType)
    {
        return _httpContextAccessor.HttpContext!.User.Claims
            .FirstOrDefault(claim => claim.Type == claimType)?.Value;
    }
}