namespace Workshopper.Application.Subscriptions.Commands.CreateSubscription;

public class CreateSubscriptionCommandHandler : CommandHandler<CreateSubscriptionCommand, Guid>
{
    private readonly ISubscriptionsWriteService _subscriptionsWriteService;

    public CreateSubscriptionCommandHandler(ISubscriptionsWriteService subscriptionsWriteService)
    {
        _subscriptionsWriteService = subscriptionsWriteService;
    }

    public override Task<Guid> ExecuteAsync(CreateSubscriptionCommand command, CancellationToken ct = new())
    {
        var subscriptionId = _subscriptionsWriteService.CreateSubscription(
            command.SubscriptionType,
            command.AdminId);

        return Task.FromResult(subscriptionId);
    }
}