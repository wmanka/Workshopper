using FluentValidation;
using Workshopper.Application.Common.Abstractions;
using Workshopper.Domain.Sessions;

namespace Workshopper.Application.Sessions.Commands.CreateSession;

public class CreateOnlineSessionCommandHandler : CommandHandler<CreateOnlineSessionCommand, Guid>
{
    private readonly IOnlineSessionsRepository _onlineSessionsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IValidator<CreateOnlineSessionCommand> _validator;

    public CreateOnlineSessionCommandHandler(
        IOnlineSessionsRepository onlineSessionsRepository,
        IUnitOfWork unitOfWork,
        ICurrentUserProvider currentUserProvider,
        IValidator<CreateOnlineSessionCommand> validator)
    {
        _onlineSessionsRepository = onlineSessionsRepository;
        _unitOfWork = unitOfWork;
        _currentUserProvider = currentUserProvider;
        _validator = validator;
    }

    public override async Task<Guid> ExecuteAsync(CreateOnlineSessionCommand command, CancellationToken ct = new())
    {
        // todo
        var validationResult = await _validator.ValidateAsync(command, ct);

        foreach (var validationFailure in validationResult.Errors)
        {
            AddError(validationFailure);
        }

        ThrowIfAnyErrors();

        var currentUser = _currentUserProvider.GetCurrentUser();

        var onlineSession = OnlineSession.Create(
            command.Title,
            command.Description,
            command.SessionType,
            command.StartDateTime,
            command.EndDateTime,
            command.Places,
            currentUser!.ProfileId!.Value,
            command.Link
        );

        await _onlineSessionsRepository.AddOnlineSessionAsync(onlineSession);
        await _unitOfWork.CommitChangesAsync();

        return onlineSession.Id;
    }
}