using Microsoft.Extensions.Logging;
using Workshopper.Domain.Sessions.Events;

namespace Workshopper.Application.Sessions.Commands.CancelSession;

public class SessionCanceledEventHandler : IEventHandler<SessionCanceledEvent>
{
    private readonly ILogger<SessionCanceledEventHandler> _logger;

    public SessionCanceledEventHandler(ILogger<SessionCanceledEventHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(SessionCanceledEvent eventModel, CancellationToken ct)
    {
        _logger.LogInformation(message: "Session '{title}' was cancelled", eventModel.Session.Title);

        return Task.CompletedTask;
    }
}