using Workshopper.Domain.Common;
using Workshopper.Domain.Users.UserProfiles;

namespace Workshopper.Domain.Users.Events;

public sealed record UserProfileCreatedDomainEvent(UserProfile UserProfile) : IDomainEvent;