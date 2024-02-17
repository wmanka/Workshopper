﻿using FluentAssertions;
using Workshopper.Domain.Notifications;

namespace Workshopper.Domain.Tests.Unit.Notifications;

public class NotificationTests
{
    [Fact]
    public void Create_ShouldReturnNotification_WhenCalled()
    {
        var notificationType = NotificationType.SessionCanceled;
        const string content = "Session canceled.";
        var userId = Guid.NewGuid();

        var notification = Notification.Create(
            notificationType,
            content,
            userId);

        notification.Should().NotBeNull();
        notification.NotificationType.Should().Be(notificationType);
        notification.Content.Should().Be(content);
        notification.UserId.Should().Be(userId);
    }

    [Fact]
    public void MarkAsRead_ShouldMarkNotificationAsRead_WhenCalled()
    {
        var notificationType = NotificationType.SessionCanceled;
        const string content = "Session canceled.";
        var userId = Guid.NewGuid();

        var notification = Notification.Create(
            notificationType,
            content,
            userId);

        notification.MarkAsRead();

        notification.IsRead.Should().BeTrue();
    }
}