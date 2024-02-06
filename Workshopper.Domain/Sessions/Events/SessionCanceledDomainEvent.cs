using Workshopper.Domain.Common;

namespace Workshopper.Domain.Sessions.Events;

public sealed record SessionCanceledDomainEvent(Guid Id) : IDomainEvent;