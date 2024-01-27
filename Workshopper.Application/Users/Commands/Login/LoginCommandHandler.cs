namespace Workshopper.Application.Users.Commands.Login;

public class LoginCommandHandler : CommandHandler<LoginCommand>
{
    public override async Task ExecuteAsync(LoginCommand command, CancellationToken ct = new())
    {
    }
}