using Microsoft.AspNetCore.SignalR;

namespace Workshopper.Api.Notifications.Notifications;

public sealed class NotificationsHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        await Clients.All.SendAsync("ReceiveNotification", $"{Context.ConnectionId} connected");
    }
}