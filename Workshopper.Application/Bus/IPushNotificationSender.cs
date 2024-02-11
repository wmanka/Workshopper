namespace Workshopper.Application.Bus;

public interface IPushNotificationSender
{
    Task SendNotificationAsync(PushNotification notification);
    Task SendNotificationAsync(PushNotification notification, IEnumerable<Guid> userIds);
}