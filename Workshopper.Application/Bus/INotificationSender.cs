namespace Workshopper.Application.Bus;

public interface INotificationSender
{
    Task SendAsync(BusNotification busNotification);
    Task SendAsync(BusNotification busNotification, IEnumerable<Guid> userIds);
}