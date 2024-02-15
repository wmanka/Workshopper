using Microsoft.AspNetCore.SignalR;
using Workshopper.Application.Bus;
using Workshopper.Application.Common.Abstractions;

namespace Workshopper.Application.Notifications;

public sealed class NotificationsHub : Hub<INotificationsHubClient>
{
    public override async Task OnConnectedAsync()
    {
        await Clients.All.ReceiveNotification(new BusNotification(
            "User connected",
            $"{Context.ConnectionId} connected"));
    }

    public async Task SendNotification(BusNotification busNotification)
    {
        await Clients.All.ReceiveNotification(busNotification);
    }
}