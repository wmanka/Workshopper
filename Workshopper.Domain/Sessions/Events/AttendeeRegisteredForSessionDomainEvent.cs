using Workshopper.Domain.Common;

namespace Workshopper.Domain.Sessions.Events;

public sealed record AttendeeRegisteredForSessionDomainEvent(Guid SessionId, Guid AttendeeProfileId) : IDomainEvent;