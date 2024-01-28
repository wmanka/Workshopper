using Workshopper.Domain.Subscriptions;

namespace Workshopper.Application.Common.Abstractions;

public interface ISubscriptionsRepository
{
    public Task AddSubscriptionAsync(Subscription subscription);
}