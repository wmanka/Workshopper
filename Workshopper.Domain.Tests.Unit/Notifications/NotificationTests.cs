using Workshopper.Domain.Notifications;

namespace Workshopper.Domain.Tests.Unit.Notifications;

public class NotificationTests
{
    [Theory]
    [MemberData(nameof(GetNotificationsMemberData))]
    public void Create_ShouldReturnNotification_WhenCalled(NotificationType notificationType, string content, Guid userId)
    {
        var notification = Notification.Create(
            notificationType,
            content,
            userId);

        notification.Should().NotBeNull();
        notification.Should().BeEquivalentTo(new
        {
            NotificationType = notificationType,
            Content = content,
            UserId = userId
        });
    }

    [Theory]
    [MemberData(nameof(GetNotificationsMemberData))]
    public void MarkAsRead_ShouldMarkNotificationAsRead_WhenCalled(NotificationType notificationType, string content, Guid userId)
    {
        var notification = Notification.Create(
            notificationType,
            content,
            userId);

        notification.MarkAsRead();

        notification.IsRead.Should().BeTrue();
    }

    public static IEnumerable<object[]> GetNotificationsMemberData =>
        new List<object[]>
        {
            new object[]
            {
                NotificationType.SessionCreated, "Session created.", Guid.NewGuid()
            },
            new object[]
            {
                NotificationType.SessionUpdated, "Session updated.", Guid.NewGuid()
            },
            new object[]
            {
                NotificationType.SessionCanceled, "Session canceled.", Guid.NewGuid()
            },
        };
}