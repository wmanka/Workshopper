using Workshopper.Application.Common.Interfaces;
using Workshopper.Domain.Subscriptions;

namespace Workshopper.Application.Subscriptions.Commands.CreateSubscription;

public class CreateSubscriptionCommandHandler : CommandHandler<CreateSubscriptionCommand, Guid>
{
    private readonly ISubscriptionsRepository _subscriptionsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSubscriptionCommandHandler(
        ISubscriptionsRepository subscriptionsRepository,
        IUnitOfWork unitOfWork)
    {
        _subscriptionsRepository = subscriptionsRepository;
        _unitOfWork = unitOfWork;
    }

    public override async Task<Guid> ExecuteAsync(CreateSubscriptionCommand command, CancellationToken ct = new())
    {
        var subscription = new Subscription(
            command.Name,
            command.SubscriptionType);

        await _subscriptionsRepository.AddSubscriptionAsync(subscription);
        await _unitOfWork.CommitChangesAsync();

        return subscription.Id;
    }
}