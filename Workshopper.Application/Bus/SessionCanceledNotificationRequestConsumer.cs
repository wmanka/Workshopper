using MassTransit;
using Microsoft.Extensions.Logging;

namespace Workshopper.Application.Bus;

public sealed class SessionCanceledNotificationRequestConsumer : IConsumer<SessionCanceledNotificationRequest>
{
    private readonly ILogger<SessionCanceledNotificationRequestConsumer> _logger;

    public SessionCanceledNotificationRequestConsumer(ILogger<SessionCanceledNotificationRequestConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<SessionCanceledNotificationRequest> context)
    {
        _logger.LogInformation("NotificationCreatedConsumer: {message}", context.Message);

        // todo: send email/push/sms notification to host/attendees that are concerned and subscribe to this event

        await Task.CompletedTask;
    }
}