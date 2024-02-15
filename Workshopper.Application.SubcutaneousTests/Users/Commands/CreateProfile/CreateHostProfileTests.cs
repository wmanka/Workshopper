using Workshopper.Application.Users.Commands.CreateProfile;
using Workshopper.Tests.Common.Constants;

namespace Workshopper.Application.SubcutaneousTests.Users.Commands.CreateProfile;

public class CreateHostProfileTests
{
    [Fact]
    public void CreateHostProfile_WhenValidCommand_ShouldCreateHostProfile()
    {
        var command = new CreateHostProfileCommand(
            Constants.UserProfiles.FirstName,
            Constants.UserProfiles.LastName,
            Constants.UserProfiles.Title,
            Constants.UserProfiles.Company,
            Constants.UserProfiles.Bio);

        // todo: mock _currentUserProvider, _usersRepository, _unitOfWork

        Assert.True(true);
    }
}