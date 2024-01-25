using Workshopper.Domain.Common;

namespace Workshopper.Domain.Sessions.Events;

public record SessionCanceledEvent(Session Session) : IEvent;