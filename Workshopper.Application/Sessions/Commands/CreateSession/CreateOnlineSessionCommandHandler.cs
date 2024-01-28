using Workshopper.Application.Common.Abstractions;
using Workshopper.Domain.Sessions;

namespace Workshopper.Application.Sessions.Commands.CreateSession;

public class CreateOnlineSessionCommandHandler : CommandHandler<CreateOnlineSessionCommand, Guid>
{
    private readonly IOnlineSessionsRepository _onlineSessionsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserProvider _currentUserProvider;

    public CreateOnlineSessionCommandHandler(
        IOnlineSessionsRepository onlineSessionsRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserProvider currentUserProvider)
    {
        _onlineSessionsRepository = onlineSessionsRepository;
        _unitOfWork = unitOfWork;
        _currentUserProvider = currentUserProvider;
    }

    public override async Task<Guid> ExecuteAsync(CreateOnlineSessionCommand command, CancellationToken ct = new())
    {
        var currentUser = _currentUserProvider.GetCurrentUser();
        if (currentUser is null || !currentUser.IsHost)
        {
            ThrowError(SessionErrors.OnlyHostCanCreateSession);
        }

        var validationResult = await new CreateOnlineSessionCommandValidator().ValidateAsync(command, ct); // todo:
        if (!validationResult.IsValid)
        {
            foreach (var validationFailure in validationResult.Errors)
            {
                AddError(validationFailure);
            }
        }

        ThrowIfAnyErrors();

        var onlineSession = new OnlineSession(
            command.Title,
            command.Description,
            command.SessionType,
            command.StartDateTime,
            command.EndDateTime,
            command.Places,
            currentUser.ProfileId!.Value,
            command.Link
        );

        await _onlineSessionsRepository.AddOnlineSessionAsync(onlineSession);
        await _unitOfWork.CommitChangesAsync();

        return onlineSession.Id;
    }
}