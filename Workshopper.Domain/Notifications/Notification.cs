using Workshopper.Domain.Common;
using Workshopper.Domain.Users;

namespace Workshopper.Domain.Notifications;

public class Notification : DomainEntity
{
    public NotificationType NotificationType { get; private set; }

    public string Content { get; private set; }

    public Guid UserId { get; private set; }

    public User User { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public bool IsRead { get; private set; }

    private Notification(
        NotificationType notificationType,
        string content,
        Guid userId,
        Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        NotificationType = notificationType;
        Content = content;
        UserId = userId;
        CreatedAt = DateTime.Now;
        IsRead = false;
    }

    public static Notification Create(
        NotificationType notificationType,
        string content,
        Guid userId,
        Guid? id = null)
    {
        var notification = new Notification(
            notificationType,
            content,
            userId,
            id);

        return notification;
    }

    public void MarkAsRead()
    {
        IsRead = true;
    }

    private Notification()
    {
    }
}