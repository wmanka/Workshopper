using Workshopper.Domain.Common;

namespace Workshopper.Domain.Sessions.Events;

public sealed record SessionCanceledDomainEvent(Session Session) : IDomainEvent;