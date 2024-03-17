using Workshopper.Application.Common.Abstractions;
using Workshopper.Application.Users.Specifications;
using Workshopper.Domain.Users;
using Workshopper.Infrastructure.FilesStore;

namespace Workshopper.Application.Users.Commands.GetImage;

public class GetUserImageCommandHandler : CommandHandler<GetUserImageCommand, FileReponse?>
{
    private readonly IFilesStore _filesStore;
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IUsersRepository _usersRepository;
    public GetUserImageCommandHandler(
        IFilesStore filesStore,
        ICurrentUserProvider currentUserProvider,
        IUsersRepository usersRepository)
    {
        _filesStore = filesStore;
        _currentUserProvider = currentUserProvider;
        _usersRepository = usersRepository;
    }

    public override async Task<FileReponse?> ExecuteAsync(GetUserImageCommand command, CancellationToken ct = new())
    {
        var currentUser = _currentUserProvider.GetCurrentUser();
        if (currentUser is null || currentUser.UserId != command.UserId )
        {
            ThrowError(UserErrors.UserUnauthorized);
        }

        var user = await _usersRepository
            .GetAsync(new UserByIdSpecification(command.UserId));

        if (user is null)
        {
            ThrowError(UserErrors.UserNotFound);
        }

        var imageFileId = user.ImageId;
        if (!imageFileId.HasValue)
        {
            ThrowError(UserErrors.UserImageNotFound);
        }

        try
        {
            return await _filesStore.DownloadAsync(imageFileId.Value);
        }
        catch (Exception)
        {
            ThrowError("User image file upload failed");
        }

        return null;
    }
}