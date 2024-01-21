using Workshopper.Application.Common.Interfaces;
using Workshopper.Domain.Sessions;

namespace Workshopper.Application.Sessions.Commands.CreateOnlineSession;

public class CreateOnlineSessionCommandHandler : CommandHandler<CreateOnlineSessionCommand, Guid>
{
    private readonly IOnlineSessionsRepository _onlineSessionsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateOnlineSessionCommandHandler(
        IOnlineSessionsRepository onlineSessionsRepository,
        IUnitOfWork unitOfWork)
    {
        _onlineSessionsRepository = onlineSessionsRepository;
        _unitOfWork = unitOfWork;
    }

    public override async Task<Guid> ExecuteAsync(CreateOnlineSessionCommand command, CancellationToken ct = new())
    {
        var onlineSession = new OnlineSession(
            command.Title,
            command.Description,
            command.SessionType,
            command.StartDateTime,
            command.EndDateTime,
            command.Places,
            command.Link
        );

        await _onlineSessionsRepository.AddOnlineSessionAsync(onlineSession);
        await _unitOfWork.CommitChangesAsync();

        return onlineSession.Id;
    }
}