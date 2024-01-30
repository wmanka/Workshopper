using Workshopper.Application.Common.Abstractions;
using Workshopper.Application.Sessions.Specifications;
using Workshopper.Application.Users.Specifications;
using Workshopper.Domain.Sessions;
using Workshopper.Domain.Users;
using Workshopper.Domain.Users.UserProfiles;

namespace Workshopper.Application.Sessions.Commands.RegisterForSession;

public class RegisterForSessionCommandHandler : CommandHandler<RegisterForSessionCommand>
{
    private readonly ISessionsRepository _sessionsRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserProvider _currentUserProvider;

    public RegisterForSessionCommandHandler(
        ISessionsRepository onlineSessionsRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserProvider currentUserProvider,
        IUsersRepository usersRepository)
    {
        _sessionsRepository = onlineSessionsRepository;
        _unitOfWork = unitOfWork;
        _currentUserProvider = currentUserProvider;
        _usersRepository = usersRepository;
    }

    public override async Task ExecuteAsync(RegisterForSessionCommand command, CancellationToken ct = new())
    {
        var currentUser = _currentUserProvider.GetCurrentUser();
        if (currentUser?.ProfileId is null || !currentUser.IsAttendee)
        {
            ThrowError(SessionErrors.OnlyAttendeesCanRegisterForSession);
        }

        var session = await _sessionsRepository.GetAsync(
            new RegisterableSessionByIdWithAttendeesAndHostSpecification(command.SessionId));

        if (session is null)
        {
            ThrowError(SessionErrors.NotFound);
        }

        var userProfile = await _usersRepository.GetProfileAsync(new UserProfileByIdSpecification(currentUser.ProfileId!.Value));
        if (userProfile is null)
        {
            ThrowError(UserErrors.UserProfileNotFound);
        }

        if (userProfile.ProfileType != ProfileType.Attendee || userProfile is not AttendeeProfile attendeeProfile)
        {
            ThrowError(SessionErrors.OnlyAttendeesCanRegisterForSession);

            return;
        }

        session.RegisterAttendee(attendeeProfile);

        _sessionsRepository.UpdateSession(session);
        await _unitOfWork.CommitChangesAsync();
    }
}