using Workshopper.Application.Common.Interfaces;
using Workshopper.Domain.Sessions;

namespace Workshopper.Application.Sessions.Commands.CreateSession;

public class CreateStationarySessionCommandHandler : CommandHandler<CreateStationarySessionCommand, Guid>
{
    private readonly IStationarySessionsRepository _stationarySessionsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateStationarySessionCommandHandler(
        IStationarySessionsRepository stationarySessionsRepository,
        IUnitOfWork unitOfWork)
    {
        _stationarySessionsRepository = stationarySessionsRepository;
        _unitOfWork = unitOfWork;
    }

    public override async Task<Guid> ExecuteAsync(CreateStationarySessionCommand command, CancellationToken ct = new())
    {
        var stationarySession = new StationarySession(
            command.Title,
            command.Description,
            command.SessionType,
            command.StartDateTime,
            command.EndDateTime,
            command.Places,
            command.Address
        );

        await _stationarySessionsRepository.AddStationarySessionAsync(stationarySession);
        await _unitOfWork.CommitChangesAsync();

        return stationarySession.Id;
    }
}