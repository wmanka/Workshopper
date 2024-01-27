using System.Security.Claims;
using FastEndpoints.Security;
using Microsoft.Extensions.Options;
using Workshopper.Application.Common.Interfaces;
using Workshopper.Domain.Users;
using Workshopper.Domain.Users.UserProfiles;
using Workshopper.Infrastructure.Common.Persistence;

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

        var roles = new List<string>
        {
            "Host"
        };

        var permissions = new List<string>
        {
            "CreateSession",
            "UpdateSession"
        };

        var token = JWTBearer.CreateToken(
            signingKey: _jwtOptions.Value.SigningKey,
            expireAt: DateTime.UtcNow.AddDays(_jwtOptions.Value.TokenExpiration),
            claims: claims,
            roles: roles,
            permissions: permissions
        );

        return token;
    }
}