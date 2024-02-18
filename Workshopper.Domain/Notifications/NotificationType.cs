namespace Workshopper.Domain.Notifications;

public sealed class NotificationType(string name, int value) : SmartEnum<NotificationType>(name, value)
{
    public readonly static NotificationType SessionCreated = new(nameof(SessionCreated), 0);
    public readonly static NotificationType SessionUpdated = new(nameof(SessionUpdated), 1);
    public readonly static NotificationType SessionCanceled = new(nameof(SessionCanceled), 2);
}