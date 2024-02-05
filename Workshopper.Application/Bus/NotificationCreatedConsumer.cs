using MassTransit;
using Microsoft.Extensions.Logging;

namespace Workshopper.Application.Bus;

public class NotificationCreatedConsumer : IConsumer<NotificationCreated>
{
    private readonly ILogger<NotificationCreatedConsumer> _logger;

    public NotificationCreatedConsumer(ILogger<NotificationCreatedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<NotificationCreated> context)
    {
        _logger.LogInformation("NotificationCreatedConsumer: {NotificationId}", context.Message);

        await Task.CompletedTask;
    }
}