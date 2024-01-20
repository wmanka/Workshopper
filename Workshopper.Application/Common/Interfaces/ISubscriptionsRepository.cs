using Workshopper.Domain.Subscriptions;

namespace Workshopper.Application.Common.Interfaces;

public interface ISubscriptionsRepository
{
    public Task AddSubscriptionAsync(Subscription subscription);
}