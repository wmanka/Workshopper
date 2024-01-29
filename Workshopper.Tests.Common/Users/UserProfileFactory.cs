using Workshopper.Domain.Users.UserProfiles;

namespace Workshopper.Tests.Common.Users;

public static class UserProfileFactory
{
    public static HostProfile CreateHostProfile(
        string? firstName = null,
        string? lastName = null,
        string? title = null,
        string? company = null,
        string? bio = null,
        Guid? id = null)
    {
        return HostProfile.Create(
            firstName: firstName ?? Constants.Constants.UserProfiles.FirstName,
            lastName: lastName ?? Constants.Constants.UserProfiles.LastName,
            title: title ?? Constants.Constants.UserProfiles.Title,
            company: company ?? Constants.Constants.UserProfiles.Company,
            bio: bio ?? Constants.Constants.UserProfiles.Bio,
            id: id ?? Constants.Constants.UserProfiles.Id);
    }
}