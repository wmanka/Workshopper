namespace Workshopper.Application.Subscriptions;

public class SubscriptionsWriteService : ISubscriptionsWriteService
{
    public Guid CreateSubscription(string subscriptionType, Guid adminId)
    {
        return Guid.NewGuid();
    }
}