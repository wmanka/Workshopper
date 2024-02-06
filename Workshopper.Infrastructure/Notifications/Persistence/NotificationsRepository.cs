using Microsoft.EntityFrameworkCore;
using Workshopper.Application.Common.Abstractions;
using Workshopper.Application.Common.Models;
using Workshopper.Domain.Notifications;
using Workshopper.Infrastructure.Common.Persistence;

namespace Workshopper.Infrastructure.Notifications.Persistence;

internal class NotificationsRepository : INotificationsRepository
{
    private readonly WorkshopperDbContext _context;

    public NotificationsRepository(WorkshopperDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Notification notification)
    {
        await _context.Notifications.AddAsync(notification);
    }

    public async Task<Notification?> GetAsync(Guid id)
    {
        return await _context.Notifications.FindAsync(id);
    }

    public async Task<Notification?> GetAsync(Specification<Notification> specification)
    {
        return await SpecificationEvaluator.GetQuery(_context.Notifications, specification).FirstOrDefaultAsync();
    }

    public async Task<List<NotificationSubscription>> GetSubscriptionsAsync(NotificationType notificationType, IEnumerable<Guid> recepientIds)
    {
        return await _context.NotificationSubscriptions
            .Where(x => x.NotificationType == notificationType
                        && recepientIds.Contains(x.UserProfileId))
            .ToListAsync();
    }
}