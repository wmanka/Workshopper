using Workshopper.Application.Common.Interfaces;
using Workshopper.Domain.Sessions;

namespace Workshopper.Application.Sessions.Commands.CreateSession;

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
        var validationResult = await new CreateOnlineSessionCommandValidator().ValidateAsync(command, ct);
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
            command.HostProfileId,
            command.Link
        );

        await _onlineSessionsRepository.AddOnlineSessionAsync(onlineSession);
        await _unitOfWork.CommitChangesAsync();

        return onlineSession.Id;
    }
}