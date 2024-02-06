using Workshopper.Domain.Users.UserProfiles;

namespace Workshopper.Domain.Notifications;

public sealed class NotificationSubscription
{
    public NotificationType NotificationType { get; private set; }

    public NotificationDeliveryType NotificationDeliveryType { get; private set; }

    public Guid UserProfileId { get; private set; }

    public UserProfile UserProfile { get; private set; }

    private NotificationSubscription()
    {
    }
}