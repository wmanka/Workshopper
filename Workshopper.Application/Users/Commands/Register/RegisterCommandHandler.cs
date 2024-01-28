using Workshopper.Application.Common.Abstractions;
using Workshopper.Application.Users.Specifications;
using Workshopper.Domain.Users;

namespace Workshopper.Application.Users.Commands.Register;

public class RegisterCommandHandler : CommandHandler<RegisterCommand>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterCommandHandler(
        IUsersRepository usersRepository,
        IUnitOfWork unitOfWork, IPasswordHasher passwordHasher)
    {
        _usersRepository = usersRepository;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
    }

    public override async Task ExecuteAsync(RegisterCommand command, CancellationToken ct = new())
    {
        var userExists = await _usersRepository
            .AnyAsync(new UserByEmailSpecification(command.Email));

        if (userExists)
        {
            ThrowError(UserErrors.UserAlreadyExists);
        }

        var hash = _passwordHasher.HashPassword(command.Password);

        var user = new User(
            command.Email,
            hash);

        await _usersRepository.AddAsync(user);
        await _unitOfWork.CommitChangesAsync();
    }
}