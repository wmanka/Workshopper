using Microsoft.AspNetCore.SignalR;
using Workshopper.Application.Common.Abstractions;
using Workshopper.Application.Notifications;

namespace Workshopper.Application.Bus;

public class PushNotificationSender : IPushNotificationSender
{
    private readonly IHubContext<NotificationsHub, INotificationsHubClient> _notificationsHub;

    public PushNotificationSender(IHubContext<NotificationsHub, INotificationsHubClient> notificationsHub)
    {
        _notificationsHub = notificationsHub;
    }

    public async Task SendNotificationAsync(PushNotification notification)
    {
        await _notificationsHub.Clients.All.ReceiveNotification(notification);
    }

    public async Task SendNotificationAsync(PushNotification notification, IEnumerable<Guid> userIds)
    {
        await _notificationsHub.Clients
            .Users(userIds.Select(x => x.ToString()))
            .ReceiveNotification(notification);
    }
}