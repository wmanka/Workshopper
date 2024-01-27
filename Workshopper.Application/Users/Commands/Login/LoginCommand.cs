namespace Workshopper.Application.Users.Commands.Login;

public record LoginCommand(string Email, string Password) : ICommand<string>;