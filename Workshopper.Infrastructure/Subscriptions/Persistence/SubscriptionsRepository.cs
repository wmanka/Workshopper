using Workshopper.Application.Common.Interfaces;
using Workshopper.Domain.Subscriptions;
using Workshopper.Infrastructure.Common.Persistence;

namespace Workshopper.Infrastructure.Subscriptions.Persistence;

internal class SubscriptionsRepository : ISubscriptionsRepository
{
    private readonly WorkshopperDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public SubscriptionsRepository(
        WorkshopperDbContext dbContext,
        IUnitOfWork unitOfWork)
    {
        _context = dbContext;
        _unitOfWork = unitOfWork;
    }

    public async Task AddSubscriptionAsync(Subscription subscription)
    {
        await _context.Subscriptions.AddAsync(subscription);
    }

    public async Task<Subscription?> GetByIdAsync(Guid subscriptionId)
    {
        return await _context.Subscriptions.FindAsync(subscriptionId);
    }
}