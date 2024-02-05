using MassTransit;
using Microsoft.Extensions.Logging;
using Workshopper.Application.Bus;
using Workshopper.Domain.Sessions.Events;

namespace Workshopper.Application.Sessions.Events;

public class SessionCanceledEventHandler : IEventHandler<SessionCanceledDomainEvent>
{
    private readonly ILogger<SessionCanceledEventHandler> _logger;
    private readonly IBus _bus;

    public SessionCanceledEventHandler(ILogger<SessionCanceledEventHandler> logger, IBus bus)
    {
        _logger = logger;
        _bus = bus;
    }

    public async Task HandleAsync(SessionCanceledDomainEvent domainEventModel, CancellationToken ct)
    {
        _logger.LogInformation("Session '{title}' was cancelled", domainEventModel.Session.Title);

        await _bus.Publish(new NotificationCreated(domainEventModel.Session.Id, "session cancelled"), ct);
    }
}