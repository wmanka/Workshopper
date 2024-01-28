using Workshopper.Domain.Users.UserProfiles;

namespace Workshopper.Application.Users.Commands.ChangeProfile;

public record ChangeProfileCommand(ProfileType ProfileType) : ICommand<string>;