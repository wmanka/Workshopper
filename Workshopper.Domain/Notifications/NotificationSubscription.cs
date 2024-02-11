using Workshopper.Domain.Users;

namespace Workshopper.Domain.Notifications;

public sealed class NotificationSubscription
{
    public NotificationType NotificationType { get; private set; }

    public NotificationDeliveryType NotificationDeliveryType { get; private set; }

    public Guid UserId { get; private set; }

    public User User { get; private set; }

    private NotificationSubscription()
    {
    }
}