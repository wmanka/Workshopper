using Microsoft.AspNetCore.SignalR;

namespace Workshopper.Api.Notifications.Notifications;

public class NotificationEndpoint : EndpointWithoutRequest
{
    private readonly IHubContext<NotificationsHub> _notificationsHub;

    public NotificationEndpoint(IHubContext<NotificationsHub> notificationsHub)
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
        await _notificationsHub.Clients.All.SendAsync(
            "ReceiveNotification",
            new
            {
                message = "Notification content"
            },
            cancellationToken: ct);

        await SendOkAsync(ct);
    }
}