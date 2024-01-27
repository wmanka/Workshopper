namespace Workshopper.Application.Users.Commands.Register;

public record RegisterCommand(string Email, string Password) : ICommand;