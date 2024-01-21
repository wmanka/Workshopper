namespace Workshopper.Application.Sessions.Commands.CancelSession;

public record CancelSessionCommand : ICommand<Guid>
{
    public Guid Id { get; init; }
}