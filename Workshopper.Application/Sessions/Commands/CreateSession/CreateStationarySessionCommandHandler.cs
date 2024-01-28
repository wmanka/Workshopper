using Workshopper.Application.Common.Abstractions;
using Workshopper.Domain.Sessions;

namespace Workshopper.Application.Sessions.Commands.CreateSession;

public class CreateStationarySessionCommandHandler : CommandHandler<CreateStationarySessionCommand, Guid>
{
    private readonly IStationarySessionsRepository _stationarySessionsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserProvider _currentUserProvider;

    public CreateStationarySessionCommandHandler(
        IStationarySessionsRepository stationarySessionsRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserProvider currentUserProvider)
    {
        _stationarySessionsRepository = stationarySessionsRepository;
        _unitOfWork = unitOfWork;
        _currentUserProvider = currentUserProvider;
    }

    public override async Task<Guid> ExecuteAsync(CreateStationarySessionCommand command, CancellationToken ct = new())
    {
        var currentUser = _currentUserProvider.GetCurrentUser();
        if (currentUser is null || !currentUser.IsHost)
        {
            ThrowError(SessionErrors.OnlyHostCanCreateSession);
        }

        // var validationResult = await new CreateStationarySessionCommandValidator().ValidateAsync(command, ct); // todo:
        // if (!validationResult.IsValid)
        // {
        //     foreach (var validationFailure in validationResult.Errors)
        //     {
        //         AddError(validationFailure);
        //     }
        // }

        var stationarySession = new StationarySession(
            command.Title,
            command.Description,
            command.SessionType,
            command.StartDateTime,
            command.EndDateTime,
            command.Places,
            currentUser.ProfileId!.Value,
            command.Address
        );

        await _stationarySessionsRepository.AddStationarySessionAsync(stationarySession);
        await _unitOfWork.CommitChangesAsync();

        return stationarySession.Id;
    }
}