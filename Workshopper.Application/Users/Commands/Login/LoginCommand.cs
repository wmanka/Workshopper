namespace Workshopper.Application.Users.Commands.Login;

public record LoginCommand(string Email, string Hash) : ICommand<string>;