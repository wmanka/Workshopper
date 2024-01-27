using Workshopper.Application.Common.Interfaces;
using Workshopper.Application.Users.Specifications;
using Workshopper.Domain.Users;

namespace Workshopper.Application.Users.Commands.Login;

public class LoginCommandHandler : CommandHandler<LoginCommand, string>
{
    private readonly IUsersRepository _usersRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginCommandHandler(
        IJwtTokenGenerator jwtTokenGenerator,
        IUsersRepository usersRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _usersRepository = usersRepository;
    }

    public override async Task<string> ExecuteAsync(LoginCommand command, CancellationToken ct = new())
    {
        var user = await _usersRepository
            .GetAsync(new UserByEmailAndHashSpecification(command.Email, command.Hash));

        if (user is null)
        {
            ThrowError(UserErrors.InvalidCredentials);

            return null;
        }

        var token = _jwtTokenGenerator.GenerateToken(user);

        return token;
    }
}