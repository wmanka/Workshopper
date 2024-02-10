namespace Workshopper.Application.Common.Abstractions;

public interface INotificationsHubClient
{
    Task ReceiveNotification(string message);
}