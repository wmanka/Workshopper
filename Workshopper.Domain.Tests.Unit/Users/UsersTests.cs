using FluentAssertions;
using Workshopper.Domain.Common;
using Workshopper.Domain.Users;
using Workshopper.Tests.Common.Users;

namespace Workshopper.Domain.Tests.Unit.Users;

public class UsersTests
{
    [Fact]
    public void CreateHostProfile_WhenHostProfileAlreadyExists_ShouldFail()
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