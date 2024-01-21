﻿using Workshopper.Domain.Sessions;

namespace Workshopper.Application.Sessions.Commands.CreateSession;

public record CreateStationarySessionCommand : ICommand<Guid>, ICreateSessionCommand
{
    public SessionType SessionType { get; init; } = null!;

    public string Title { get; init; } = null!;

    public string? Description { get; init; }

    public DateTimeOffset StartDateTime { get; init; }

    public DateTimeOffset EndDateTime { get; init; }

    public int Places { get; init; }

    public string Address { get; init; } = null!;
}