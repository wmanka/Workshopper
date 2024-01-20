namespace Workshopper.Application.Subscriptions;

public interface ISubscriptionsWriteService
{
    Guid CreateSubscription(string subscriptionType, Guid adminId);
}

// CQRS - seperate writes from reads
// CQS - return void if changing state, return value if only reading