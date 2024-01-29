using Workshopper.Application.Common.Models;
using Workshopper.Domain.Users.UserProfiles;

namespace Workshopper.Application.Users.Specifications;

internal class UserProfileByIdSpecification : Specification<UserProfile>
{
    public UserProfileByIdSpecification(Guid id)
    {
        AddFilter(userProfile => userProfile.Id == id);
    }
}