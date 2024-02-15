using Microsoft.AspNetCore.SignalR;
using Workshopper.Application.Common.Abstractions;
using Workshopper.Application.Notifications;

namespace Workshopper.Application.Bus;

public class NotificationSender : INotificationSender
{
    private readonly IHubContext<NotificationsHub, INotificationsHubClient> _notificationsHub;

    public NotificationSender(IHubContext<NotificationsHub, INotificationsHubClient> notificationsHub)
    {
        _notificationsHub = notificationsHub;
    }

    public async Task SendAsync(BusNotification busNotification)
    {
        await _notificationsHub.Clients.All.ReceiveNotification(busNotification);
    }

    public async Task SendAsync(BusNotification busNotification, IEnumerable<Guid> userIds)
    {
        await _notificationsHub.Clients
            .Users(userIds.Select(x => x.ToString()))
            .ReceiveNotification(busNotification);
    }
}