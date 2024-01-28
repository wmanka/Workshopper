using Microsoft.Extensions.Logging;
using Workshopper.Domain.Sessions.Events;

namespace Workshopper.Application.Sessions.Commands.CancelSession;

public class SessionCanceledEventHandler : IEventHandler<SessionCanceledDomainEvent>
{
    private readonly ILogger<SessionCanceledEventHandler> _logger;

    public SessionCanceledEventHandler(ILogger<SessionCanceledEventHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(SessionCanceledDomainEvent domainEventModel, CancellationToken ct)
    {
        _logger.LogInformation(
            "Session '{title}' was cancelled", domainEventModel.Session.Title);

        return Task.CompletedTask;
    }
}