using Microsoft.AspNetCore.SignalR;

namespace Workshopper.Api.Notifications.Notifications;

public sealed class NotificationsHub : Hub<INotificationsHubClient>
{
    public override async Task OnConnectedAsync()
    {
        await Clients.All.ReceiveNotification($"{Context.ConnectionId} connected");
    }

    public async Task SendNotification(string message)
    {
        await Clients.All.ReceiveNotification(message);
    }
}