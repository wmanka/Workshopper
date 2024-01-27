using Workshopper.Application.Common.Interfaces;
using Workshopper.Application.Users.Specifications;
using Workshopper.Domain.Users;

namespace Workshopper.Application.Users.Commands.Register;

public class RegisterCommandHandler : CommandHandler<RegisterCommand>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterCommandHandler(
        IUsersRepository usersRepository,
        IUnitOfWork unitOfWork)
    {
        _usersRepository = usersRepository;
        _unitOfWork = unitOfWork;
    }

    public override async Task ExecuteAsync(RegisterCommand command, CancellationToken ct = new())
    {
        var userExists = await _usersRepository
            .AnyAsync(new UserByEmailSpecification(command.Email));

        if (userExists)
        {
            ThrowError(UserErrors.UserAlreadyExists);
        }

        var user = new User(
            command.Email,
            command.Password);

        await _usersRepository.AddAsync(user);
        await _unitOfWork.CommitChangesAsync();
    }
}