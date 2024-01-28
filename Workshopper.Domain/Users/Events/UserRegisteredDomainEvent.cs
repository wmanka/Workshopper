using Workshopper.Domain.Common;

namespace Workshopper.Domain.Users.Events;

public sealed record UserRegisteredDomainEvent(User User) : IDomainEvent;