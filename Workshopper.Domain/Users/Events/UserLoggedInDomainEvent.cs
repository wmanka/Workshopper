using Workshopper.Domain.Common;

namespace Workshopper.Domain.Users.Events;

public sealed record UserLoggedInDomainEvent(User User) : IDomainEvent;