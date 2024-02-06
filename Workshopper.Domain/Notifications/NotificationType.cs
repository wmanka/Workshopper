namespace Workshopper.Domain.Notifications;

public sealed class NotificationType(string name, int value) : SmartEnum<NotificationType>(name, value)
{
    public readonly static NotificationType Application = new(nameof(Application), 0);
    public readonly static NotificationType Email = new(nameof(Email), 1);
    public readonly static NotificationType Sms = new(nameof(Sms), 2);
}