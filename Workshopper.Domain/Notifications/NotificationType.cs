namespace Workshopper.Domain.Notifications;

public sealed class NotificationType(string name, int value) : SmartEnum<NotificationType>(name, value)
{
    public readonly static NotificationType SessionCanceled = new(nameof(SessionCanceled), 0);
}