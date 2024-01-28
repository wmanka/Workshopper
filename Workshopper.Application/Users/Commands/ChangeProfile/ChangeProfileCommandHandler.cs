using Workshopper.Application.Common.Abstractions;
using Workshopper.Application.Users.Specifications;
using Workshopper.Domain.Users;

namespace Workshopper.Application.Users.Commands.ChangeProfile;

public class ChangeProfileCommandHandler : CommandHandler<ChangeProfileCommand, string>
{
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IUsersRepository _usersRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;


    public ChangeProfileCommandHandler(
        IUsersRepository usersRepository,
        ICurrentUserProvider currentUserProvider,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _usersRepository = usersRepository;
        _currentUserProvider = currentUserProvider;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public override async Task<string> ExecuteAsync(ChangeProfileCommand command, CancellationToken ct = new())
    {
        var currentUser = _currentUserProvider.GetCurrentUser();
        if (currentUser is null)
        {
            ThrowError("Unauthorized");
        }

        var user = await _usersRepository
            .GetAsync(new UserByIdSpecification(currentUser.UserId));

        if (user is null)
        {
            ThrowError(UserErrors.UserNotFound);
        }

        var userProfile = await _usersRepository
            .GetProfileAsync(new UserProfileByUserIdAndTypeSpecification(currentUser.UserId, command.ProfileType));

        if (userProfile is null)
        {
            ThrowError(UserErrors.UserNotFound);
        }

        var token = _jwtTokenGenerator.GenerateToken(user, userProfile);

        return token;
    }
}