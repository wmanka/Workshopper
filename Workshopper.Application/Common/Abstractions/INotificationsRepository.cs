using Workshopper.Application.Common.Models;
using Workshopper.Domain.Notifications;

namespace Workshopper.Application.Common.Abstractions;

public interface INotificationsRepository
{
    Task AddAsync(Notification notification);
    public Task<Notification?> GetAsync(Guid id);
    public Task<Notification?> GetAsync(Specification<Notification> specification);

    public Task<List<NotificationSubscription>> GetSubscriptionsAsync(NotificationType notificationType, IEnumerable<Guid> recepientIds);
}