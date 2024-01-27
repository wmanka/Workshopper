using Workshopper.Domain.Users;
using Workshopper.Domain.Users.UserProfiles;

namespace Workshopper.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user, UserProfile? userProfile = null);
}