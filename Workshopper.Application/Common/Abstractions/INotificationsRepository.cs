using Workshopper.Application.Common.Models;
using Workshopper.Domain.Notifications;

namespace Workshopper.Application.Common.Abstractions;

public interface INotificationsRepository
{
    Task AddAsync(Notification notification);
    Task<Notification?> GetAsync(Guid id);
    Task<Notification?> GetAsync(Specification<Notification> specification);

    Task<List<NotificationSubscription>> GetSubscriptionsAsync(NotificationType notificationType, IEnumerable<Guid> userIds);
}