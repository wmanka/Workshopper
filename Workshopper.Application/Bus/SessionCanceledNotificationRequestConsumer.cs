using System.Collections.Immutable;
using MassTransit;
using Workshopper.Application.Common.Abstractions;
using Workshopper.Application.Sessions.Specifications;
using Workshopper.Domain.Common;
using Workshopper.Domain.Notifications;
using Workshopper.Domain.Sessions;

namespace Workshopper.Application.Bus;

public sealed class SessionCanceledNotificationRequestConsumer : IConsumer<SessionCanceledNotificationRequest>
{
    private readonly INotificationsRepository _notificationsRepository;
    private readonly ISessionsRepository _sessionsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPushNotificationSender _pushNotificationSender;


    public SessionCanceledNotificationRequestConsumer(
        INotificationsRepository notificationsRepository,
        IUnitOfWork unitOfWork,
        ISessionsRepository sessionsRepository,
        IPushNotificationSender pushNotificationSender)
    {
        _notificationsRepository = notificationsRepository;
        _unitOfWork = unitOfWork;
        _sessionsRepository = sessionsRepository;
        _pushNotificationSender = pushNotificationSender;
    }


    public async Task Consume(ConsumeContext<SessionCanceledNotificationRequest> context)
    {
        var request = context.Message;

        var session = await _sessionsRepository.GetAsync(new SessionByIdWithAttendeesAndHostProfileSpecification(request.Id));
        if (session is null)
        {
            throw new DomainException(SessionErrors.NotFound);
        }

        var attendiesIds = session.Attendees.Select(a => a.UserId);
        var notificationSubscriptions = await _notificationsRepository
            .GetSubscriptionsAsync(NotificationType.SessionCanceled, attendiesIds); // todo: + sub delivery type

        if (notificationSubscriptions.Count == 0)
        {
            return;
        }

        var userIds = notificationSubscriptions
            .Select(x => x.UserId)
            .Distinct()
            .ToImmutableList();

        foreach (var userId in userIds)
        {
            var content = $"Session {request.Title} has been canceled"; // todo: content builder

            var notification = Notification.Create(
                NotificationType.SessionCanceled,
                content,
                userId);

            await _notificationsRepository.AddAsync(notification);
        }

        await _unitOfWork.CommitChangesAsync();

        var webNotification = new PushNotification(
            $"Session has been canceled",
            $"Session '{request.Title}' with {request.HostName} has been canceled");

        await _pushNotificationSender.SendNotificationAsync(webNotification, userIds);
    }
}