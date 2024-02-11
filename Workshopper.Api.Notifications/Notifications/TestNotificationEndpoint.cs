using Workshopper.Application.Bus;

namespace Workshopper.Api.Notifications.Notifications;

public class TestNotificationEndpoint : EndpointWithoutRequest
{
    private readonly IPushNotificationSender _pushNotificationSender;

    public TestNotificationEndpoint(IPushNotificationSender pushNotificationSender)
    {
        _pushNotificationSender = pushNotificationSender;
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
        await _pushNotificationSender.SendNotificationAsync(new PushNotification(
            "Test title",
            "Test content"));

        await SendNoContentAsync(ct);
    }
}