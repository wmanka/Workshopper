using FluentAssertions;
using Workshopper.Domain.Common;
using Workshopper.Domain.Users;
using Workshopper.Tests.Common.Users;

namespace Workshopper.Domain.Tests.Unit.Users.UserProfiles;

public class HostProfileTests
{
    [Fact]
    public void CreateHostProfile_ShouldThrowDomainException_WhenHostProfileAlreadyExists()
    {
        var user = UserFactory.CreateUser(
            hostProfile: UserProfileFactory.CreateHostProfile());

        var action = () => user.CreateHostProfile("John", "Smith");

        action
            .Should()
            .Throw<DomainException>()
            .WithMessage(UserErrors.UserProfileAlreadyExists);
    }
}