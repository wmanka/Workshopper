using Workshopper.Domain.Subscriptions;

namespace Workshopper.Application.Common.Abstractions;

public interface ISubscriptionsRepository
{
    Task AddSubscriptionAsync(Subscription subscription);
}