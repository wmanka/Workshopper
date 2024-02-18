using Workshopper.Application.Common.Abstractions;
using Workshopper.Domain.Common;

namespace Workshopper.Application.Sessions.Commands.CancelSession;

public class CancelSessionCommandHandler : CommandHandler<CancelSessionCommand, Guid>
{
    private readonly ISessionsRepository _sessionsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CancelSessionCommandHandler(
        ISessionsRepository sessionsRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
    {
        _sessionsRepository = sessionsRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public override async Task<Guid> ExecuteAsync(CancelSessionCommand command, CancellationToken ct = new())
    {
        var session = await _sessionsRepository.GetAsync(command.Id);
        if (session is null)
        {
            ThrowError("Session not found");
        }

        try
        {
            session.Cancel(_dateTimeProvider);
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