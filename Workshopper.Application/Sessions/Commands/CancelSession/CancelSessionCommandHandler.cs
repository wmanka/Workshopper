using Workshopper.Application.Common.Interfaces;

namespace Workshopper.Application.Sessions.Commands.CancelSession;

public class CancelSessionCommandHandler : CommandHandler<CancelSessionCommand, Guid>
{
    private readonly ISessionsRepository _sessionsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CancelSessionCommandHandler(
        ISessionsRepository sessionsRepository,
        IUnitOfWork unitOfWork)
    {
        _sessionsRepository = sessionsRepository;
        _unitOfWork = unitOfWork;
    }

    public override async Task<Guid> ExecuteAsync(CancelSessionCommand command, CancellationToken ct = new())
    {
        var session = await _sessionsRepository.GetSessionAsync(command.Id);
        if (session is null)
        {
            ThrowError("Session not found");
        }

        session.Cancel();

        _sessionsRepository.UpdateSession(session);
        await _unitOfWork.CommitChangesAsync();

        return session.Id;
    }
}