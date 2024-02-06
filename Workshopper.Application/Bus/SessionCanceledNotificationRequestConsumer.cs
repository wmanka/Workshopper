using MassTransit;
using Microsoft.Extensions.Logging;
using Workshopper.Application.Common.Abstractions;
using Workshopper.Application.Sessions.Specifications;
using Workshopper.Domain.Common;
using Workshopper.Domain.Notifications;
using Workshopper.Domain.Sessions;

namespace Workshopper.Application.Bus;

public sealed class SessionCanceledNotificationRequestConsumer : IConsumer<SessionCanceledNotificationRequest>
{
    private readonly ILogger<SessionCanceledNotificationRequestConsumer> _logger;
    private readonly INotificationsRepository _notificationsRepository;
    private readonly ISessionsRepository _sessionsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SessionCanceledNotificationRequestConsumer(
        ILogger<SessionCanceledNotificationRequestConsumer> logger,
        INotificationsRepository notificationsRepository,
        IUnitOfWork unitOfWork, ISessionsRepository sessionsRepository)
    {
        _logger = logger;
        _notificationsRepository = notificationsRepository;
        _unitOfWork = unitOfWork;
        _sessionsRepository = sessionsRepository;
    }

    public async Task Consume(ConsumeContext<SessionCanceledNotificationRequest> context)
    {
        var request = context.Message;

        var session = await _sessionsRepository.GetAsync(new SessionByIdWithAttendeesAndHostProfileSpecification(request.Id));
        if (session is null)
        {
            throw new DomainException(SessionErrors.NotFound);
        }

        var attendiesIds = session.Attendees.Select(a => a.Id);

        var notificationSubscriptions = await _notificationsRepository
            .GetSubscriptionsAsync(NotificationType.SessionCanceled, attendiesIds);

        if (!notificationSubscriptions.Any())
        {
            return;
        }

        var recepients = notificationSubscriptions.DistinctBy(x => x.UserProfileId);
        foreach (var recepient in recepients)
        {
            var content = $"Session {request.Title} has been canceled"; // todo: content builder

            var notification = Notification.Create(
                NotificationType.SessionCanceled,
                content,
                recepient.UserProfileId);

            await _notificationsRepository.AddAsync(notification);
            await _unitOfWork.CommitChangesAsync();
        }

        await Task.CompletedTask;
    }
}