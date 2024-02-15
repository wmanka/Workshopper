using Workshopper.Application.Common.Abstractions;
using Workshopper.Application.Users.Specifications;
using Workshopper.Domain.Users;

namespace Workshopper.Application.Users.Commands.CreateProfile;

public class CreateHostProfileCommandHandler : CommandHandler<CreateHostProfileCommand, Guid>
{
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IUsersRepository _usersRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateHostProfileCommandHandler(
        IUsersRepository usersRepository,
        ICurrentUserProvider currentUserProvider, IUnitOfWork unitOfWork)
    {
        _usersRepository = usersRepository;
        _currentUserProvider = currentUserProvider;
        _unitOfWork = unitOfWork;
    }

    public override async Task<Guid> ExecuteAsync(CreateHostProfileCommand command, CancellationToken ct = new())
    {
        var currentUser = _currentUserProvider.GetCurrentUser();
        if (currentUser is null)
        {
            ThrowError(UserErrors.UserUnauthorized);
        }

        var user = await _usersRepository
            .GetAsync(new UserByIdSpecification(currentUser.UserId));

        if (user is null)
        {
            ThrowError(UserErrors.UserNotFound);
        }

        user.CreateHostProfile(
            command.FirstName,
            command.LastName,
            command.Title,
            command.Company,
            command.Bio);

        _usersRepository.Update(user);
        await _unitOfWork.CommitChangesAsync();

        return user.HostProfile!.Id;
    }
}