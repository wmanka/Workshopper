using MassTransit;
using Microsoft.Extensions.Logging;
using Workshopper.Application.Bus;
using Workshopper.Application.Common.Abstractions;
using Workshopper.Application.Sessions.Specifications;
using Workshopper.Domain.Common;
using Workshopper.Domain.Sessions;
using Workshopper.Domain.Sessions.Events;

namespace Workshopper.Application.Sessions.Events;

public class SessionCanceledEventHandler : IEventHandler<SessionCanceledDomainEvent>
{
    private readonly ILogger<SessionCanceledEventHandler> _logger;
    private readonly IBus _bus;
    private readonly ISessionsRepository _sessionsRepository;

    public SessionCanceledEventHandler(
        ILogger<SessionCanceledEventHandler> logger,
        IBus bus,
        ISessionsRepository sessionsRepository)
    {
        _logger = logger;
        _bus = bus;
        _sessionsRepository = sessionsRepository;
    }

    public async Task HandleAsync(SessionCanceledDomainEvent domainEvent, CancellationToken ct)
    {
        var session = await _sessionsRepository.GetAsync(new SessionByIdWithAttendeesAndHostProfileSpecification(domainEvent.Id));
        if (session is null)
        {
            throw new DomainException(SessionErrors.NotFound);
        }

        var notification = new SessionCanceledNotificationRequest(
            session.Id,
            session.Title,
            session.HostProfileId,
            session.HostProfile.FullName,
            (session.StartDateTime, session.EndDateTime),
            session.Attendees.Select(a => a.Id));

        await _bus.Publish(notification, ct);
    }
}