using Workshopper.Domain.Subscriptions;

namespace Workshopper.Application.Subscriptions.Commands.CreateSubscription;

public record CreateSubscriptionCommand : ICommand<Guid>
{
    public string Name { get; init; } = null!;

    public SubscriptionType SubscriptionType { get; init; } = null!;
}