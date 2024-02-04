namespace Workshopper.Api.Notifications.Notifications;

public interface INotificationsHubClient
{
    Task ReceiveNotification(string message);
}