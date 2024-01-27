using Workshopper.Domain.Common;

namespace Workshopper.Domain.Users.Events;

public record UserRegisteredEvent(User User) : IDomainEvent;