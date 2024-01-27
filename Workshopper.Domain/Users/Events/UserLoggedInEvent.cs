using Workshopper.Domain.Common;

namespace Workshopper.Domain.Users.Events;

public record UserLoggedInEvent(User User) : IDomainEvent;