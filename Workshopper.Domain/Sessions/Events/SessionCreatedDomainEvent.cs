using Workshopper.Domain.Common;

namespace Workshopper.Domain.Sessions.Events;

public sealed record SessionCreatedDomainEvent(Guid SessionId) : IDomainEvent;