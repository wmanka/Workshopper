using Workshopper.Application.Bus;

namespace Workshopper.Api.Notifications.Notifications;

public class TestNotificationEndpoint : EndpointWithoutRequest
{
    private readonly INotificationSender _notificationSender;

    public TestNotificationEndpoint(INotificationSender notificationSender)
    {
        _notificationSender = notificationSender;
    }

    public override void Configure()
    {
        Get("/notifications/test");
        AllowAnonymous();

        Description(b => b
            .ProducesProblemDetails(400, "application/json+problem"));
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await _notificationSender.SendAsync(new BusNotification(
            "Test title",
            "Test content"));

        await SendNoContentAsync(ct);
    }
}