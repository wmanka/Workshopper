using Microsoft.AspNetCore.SignalR;

namespace Workshopper.Api.Notifications.Notifications;

public class NotificationEndpoint : EndpointWithoutRequest
{
    private readonly IHubContext<NotificationsHub, INotificationsHubClient> _notificationsHub;

    public NotificationEndpoint(IHubContext<NotificationsHub, INotificationsHubClient> notificationsHub)
    {
        _notificationsHub = notificationsHub;
    }

    public override void Configure()
    {
        Get("/notifications");
        AllowAnonymous();

        Description(b => b
            .ProducesProblemDetails(400, "application/json+problem"));
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await _notificationsHub.Clients.All.ReceiveNotification("Test notification");

        await SendNoContentAsync(ct);
    }
}