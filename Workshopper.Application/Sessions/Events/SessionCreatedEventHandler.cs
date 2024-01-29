using Microsoft.Extensions.Logging;
using Workshopper.Domain.Sessions.Events;

namespace Workshopper.Application.Sessions.Events;

public class SessionCreatedEventHandler : IEventHandler<SessionCreatedDomainEvent>
{
    private readonly ILogger<SessionCreatedEventHandler> _logger;

    public SessionCreatedEventHandler(ILogger<SessionCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(SessionCreatedDomainEvent domainEventModel, CancellationToken ct)
    {
        _logger.LogInformation(
            "Session with id '{id}' was created", domainEventModel.SessionId);

        return Task.CompletedTask;
    }
}