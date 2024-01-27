using Workshopper.Domain.Sessions;

namespace Workshopper.Application.Sessions.Commands.CreateSession;

public record CreateOnlineSessionCommand : ICreateSessionCommand
{
    public SessionType SessionType { get; init; } = null!;

    public string Title { get; init; } = null!;

    public string? Description { get; init; }

    public DateTimeOffset StartDateTime { get; init; }

    public DateTimeOffset EndDateTime { get; init; }

    public int Places { get; init; }

    public Guid HostProfileId { get; set; }

    public string Link { get; init; } = null!;
}