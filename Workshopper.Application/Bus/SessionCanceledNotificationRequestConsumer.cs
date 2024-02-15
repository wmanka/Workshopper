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
    private readonly INotificationSender _notificationSender;
    private readonly IEmailSender _emailSender;


    public SessionCanceledNotificationRequestConsumer(
        INotificationsRepository notificationsRepository,
        IUnitOfWork unitOfWork,
        ISessionsRepository sessionsRepository,
        INotificationSender notificationSender,
        IEmailSender emailSender)
    {
        _notificationsRepository = notificationsRepository;
        _unitOfWork = unitOfWork;
        _sessionsRepository = sessionsRepository;
        _notificationSender = notificationSender;
        _emailSender = emailSender;
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

        // todo: refactor

        var notificationSubscriptions = await _notificationsRepository
            .GetSubscriptionsAsync(NotificationType.SessionCanceled, attendiesIds);

        var usersWithNotificationType = notificationSubscriptions
            .Select(x => new
            {
                x.UserId,
                x.NotificationDeliveryType
            })
            .ToImmutableList();

        foreach (var userWithNotificationType in usersWithNotificationType)
        {
            switch (userWithNotificationType.NotificationDeliveryType.Name)
            {
                case nameof(NotificationDeliveryType.Email):
                {
                    await _emailSender.SendAsync(new Email());

                    break;
                }
                case nameof(NotificationDeliveryType.Application):
                {
                    var content = $"Session {request.Title} with {request.HostName} has been canceled"; // todo: content builder

                    var notification = Notification.Create(
                        NotificationType.SessionCanceled,
                        content,
                        userWithNotificationType.UserId);

                    await _notificationsRepository.AddAsync(notification);

                    break;
                }
                case nameof(NotificationDeliveryType.Sms):
                {
                    break;
                }
            }
        }

        await _unitOfWork.CommitChangesAsync();

        // signalr

        var webNotification = new BusNotification(
            $"Session has been canceled",
            $"Session '{request.Title}' with {request.HostName} has been canceled"); // todo: content builder

        await _notificationSender.SendAsync(webNotification,
            usersWithNotificationType
                .Where(x => x.NotificationDeliveryType == NotificationDeliveryType.Application)
                .Select(x => x.UserId));
    }
}