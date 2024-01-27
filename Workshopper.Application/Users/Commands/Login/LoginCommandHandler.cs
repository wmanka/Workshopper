using Workshopper.Application.Common.Interfaces;
using Workshopper.Application.Users.Specifications;
using Workshopper.Domain.Common.Interfaces;
using Workshopper.Domain.Users;

namespace Workshopper.Application.Users.Commands.Login;

public class LoginCommandHandler : CommandHandler<LoginCommand, string>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IPasswordHasher _passwordHasher;

    public LoginCommandHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IUsersRepository usersRepository,
        IPasswordHasher passwordHasher)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _usersRepository = usersRepository;
        _passwordHasher = passwordHasher;
    }

    public override async Task<string> ExecuteAsync(LoginCommand command, CancellationToken ct = new())
    {
        var user = await _usersRepository
            .GetAsync(new UserByEmailSpecification(command.Email));

        if (user is null || !_passwordHasher.IsCorrectPassword(command.Password, user.Hash))
        {
            ThrowError(UserErrors.InvalidCredentials);
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        return token;
    }
}