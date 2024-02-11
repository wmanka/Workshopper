using Microsoft.AspNetCore.SignalR;
using Workshopper.Application.Bus;
using Workshopper.Application.Common.Abstractions;

namespace Workshopper.Application.Notifications;

public sealed class NotificationsHub : Hub<INotificationsHubClient>
{
    public override async Task OnConnectedAsync()
    {
        await Clients.All.ReceiveNotification(new PushNotification(
            "User connected",
            $"{Context.ConnectionId} connected"));
    }

    public async Task SendNotification(PushNotification notification)
    {
        await Clients.All.ReceiveNotification(notification);
    }
}