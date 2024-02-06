using Workshopper.Domain.Common;
using Workshopper.Domain.Users.UserProfiles;

namespace Workshopper.Domain.Notifications;

public class Notification : DomainEntity
{
    public NotificationType NotificationType { get; private set; }

    public string Content { get; private set; }

    public Guid RecepientId { get; private set; }

    public UserProfile Recepient { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public bool IsRead { get; private set; }

    private Notification(
        NotificationType notificationType,
        string content,
        Guid recepientId,
        Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        NotificationType = notificationType;
        Content = content;
        RecepientId = recepientId;
        CreatedAt = DateTime.Now;
        IsRead = false;
    }

    public static Notification Create(
        NotificationType notificationType,
        string content,
        Guid recepientId,
        Guid? id = null)
    {
        var notification = new Notification(
            notificationType,
            content,
            recepientId,
            id);

        return notification;
    }

    private Notification()
    {
    }
}