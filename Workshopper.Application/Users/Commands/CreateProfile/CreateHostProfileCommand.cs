namespace Workshopper.Application.Users.Commands.CreateProfile;

public record CreateHostProfileCommand(string FirstName, string LastName, string? Title, string? Company, string? Bio) : ICommand<Guid>;