using Workshopper.Domain.Sessions.Events;

namespace Workshopper.Application.Sessions.EventHandlers;

public class SessionCanceledEventHandler : IEventHandler<SessionCanceledEvent>
{
    public Task HandleAsync(SessionCanceledEvent eventModel, CancellationToken ct)
    {
        throw new NotImplementedException();
    }
}