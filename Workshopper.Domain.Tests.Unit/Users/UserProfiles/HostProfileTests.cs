using FluentAssertions;
using Workshopper.Domain.Common;
using Workshopper.Domain.Users;
using Workshopper.Domain.Users.Events;
using Workshopper.Tests.Common.Constants;
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

    [Fact]
    public void CreateHostProfile_ShouldCreateNewHostProfile_WhenArgumentsAreValid()
    {
        const string firstName = Constants.UserProfiles.FirstName;
        const string lastName = Constants.UserProfiles.LastName;
        var user = UserFactory.CreateUser();

        var action = () => user.CreateHostProfile(firstName, lastName);

        action.Should().NotThrow<DomainException>();
        user.HostProfile.Should().NotBeNull();
        user.HostProfile.Should().BeEquivalentTo(new
        {
            FirstName = firstName,
            LastName = lastName
        });
    }

    [Fact]
    public void CreateHostProfile_ShouldRaiseSessionCanceledDomainEvent_WhenArgumentsAreValid()
    {
        const string firstName = Constants.UserProfiles.FirstName;
        const string lastName = Constants.UserProfiles.LastName;
        var user = UserFactory.CreateUser();

        user.CreateHostProfile(firstName, lastName);

        user.HostProfile!.GetDomainEvents.Should().ContainEquivalentOf(new UserProfileCreatedDomainEvent(user.HostProfile));
    }
}