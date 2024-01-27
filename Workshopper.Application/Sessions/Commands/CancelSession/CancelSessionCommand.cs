namespace Workshopper.Application.Sessions.Commands.CancelSession;

public record CancelSessionCommand(Guid Id) : ICommand<Guid>;