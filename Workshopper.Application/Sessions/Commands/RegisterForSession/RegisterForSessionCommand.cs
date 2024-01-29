namespace Workshopper.Application.Sessions.Commands.RegisterForSession;

public record RegisterForSessionCommand(Guid SessionId) : ICommand;