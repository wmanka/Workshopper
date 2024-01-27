using Workshopper.Application.Common.Interfaces;
using Workshopper.Domain.Common;

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

        try
        {
            session.Cancel();
        }
        catch (DomainException ex)
        {
            ThrowError(ex.Message);
        }

        _sessionsRepository.UpdateSession(session);
        await _unitOfWork.CommitChangesAsync();

        return session.Id;
    }
}