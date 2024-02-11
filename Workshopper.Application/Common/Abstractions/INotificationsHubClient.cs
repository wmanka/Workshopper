using Workshopper.Application.Bus;

namespace Workshopper.Application.Common.Abstractions;

public interface INotificationsHubClient
{
    Task ReceiveNotification(PushNotification notification);
}