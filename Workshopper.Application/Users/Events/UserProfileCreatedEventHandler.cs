using Microsoft.Extensions.Logging;
using Workshopper.Domain.Users.Events;

namespace Workshopper.Application.Users.Events;

public class UserProfileCreatedEventHandler : IEventHandler<UserProfileCreatedDomainEvent>
{
    private readonly ILogger<UserProfileCreatedEventHandler> _logger;

    public UserProfileCreatedEventHandler(ILogger<UserProfileCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task HandleAsync(UserProfileCreatedDomainEvent domainEventModel, CancellationToken ct)
    {
        _logger.LogInformation(
            "{profileType} profile for user with id '{userId}' created successfully",
            domainEventModel.UserProfile.ProfileType.Name,
            domainEventModel.UserProfile.Id);

        return Task.CompletedTask;
    }
}