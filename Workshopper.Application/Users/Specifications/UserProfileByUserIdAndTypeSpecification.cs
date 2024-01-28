using Workshopper.Application.Common.Models;
using Workshopper.Domain.Users.UserProfiles;

namespace Workshopper.Application.Users.Specifications;

internal class UserProfileByUserIdAndTypeSpecification : Specification<UserProfile>
{
    public UserProfileByUserIdAndTypeSpecification(Guid userId, ProfileType profileType)
    {
        AddFilter(userProfile => userProfile.UserId == userId
                                 && userProfile.ProfileType == profileType);
    }
}