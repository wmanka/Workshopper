namespace Workshopper.Domain.Notifications;

public sealed class NotificationDeliveryType(string name, int value) : SmartEnum<NotificationDeliveryType>(name, value)
{
    public readonly static NotificationDeliveryType Application = new(nameof(Application), 0);
    public readonly static NotificationDeliveryType Email = new(nameof(Email), 1);
    public readonly static NotificationDeliveryType Sms = new(nameof(Sms), 2);
}