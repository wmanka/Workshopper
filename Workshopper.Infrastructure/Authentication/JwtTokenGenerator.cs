using System.Security.Claims;
using FastEndpoints.Security;
using Microsoft.Extensions.Options;
using Workshopper.Application.Common.Abstractions;
using Workshopper.Application.Common.Models;
using Workshopper.Domain.Users;
using Workshopper.Domain.Users.UserProfiles;

namespace Workshopper.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IOptions<JwtOptions> _jwtOptions;

    public JwtTokenGenerator(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions;
    }

    public string GenerateToken(User user, UserProfile? userProfile = null)
    {
        var token = JWTBearer.CreateToken(
            signingKey: _jwtOptions.Value.SigningKey,
            expireAt: DateTime.UtcNow.AddDays(_jwtOptions.Value.TokenExpiration),
            claims: GetClaims(user, userProfile),
            roles: GetRoles(userProfile)
        );

        return token;
    }

    private static IEnumerable<string> GetRoles(UserProfile? userProfile)
    {
        var roles = new List<string>();

        switch (userProfile?.ProfileType.Name)
        {
            case nameof(ProfileType.Host):
                roles.Add(DomainRoles.Host);
                break;
            case nameof(ProfileType.Attendee):
                roles.Add(DomainRoles.Attendee);
                break;
            case nameof(ProfileType.Worker):
                roles.Add(DomainRoles.Worker);
                // todo: permissions
                break;
        }

        return roles;
    }

    private static List<Claim> GetClaims(User user, UserProfile? userProfile)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Email),
            new(CustomClaimType.UserId, user.Id.ToString())
        };

        if (userProfile != null)
        {
            claims.Add(new(CustomClaimType.ProfileId, userProfile.Id.ToString()));
            claims.Add(new(CustomClaimType.ProfileType, userProfile.ProfileType.Name));
            claims.Add(new(ClaimTypes.Name, userProfile.FirstName));
            claims.Add(new(ClaimTypes.Surname, userProfile.LastName));
        }

        return claims;
    }
}