using Workshopper.Application.Common.Abstractions;
using Workshopper.Application.Users.Specifications;
using Workshopper.Domain.Users;
using Workshopper.Infrastructure.FilesStore;

namespace Workshopper.Application.Users.Commands.UploadImage;

public class UploadUserImageCommandHandler : CommandHandler<UploadUserImageCommand, Guid>
{
    private readonly IFilesStore _filesStore;
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IUsersRepository _usersRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UploadUserImageCommandHandler(
        IFilesStore filesStore,
        ICurrentUserProvider currentUserProvider,
        IUsersRepository usersRepository,
        IUnitOfWork unitOfWork)
    {
        _filesStore = filesStore;
        _currentUserProvider = currentUserProvider;
        _usersRepository = usersRepository;
        _unitOfWork = unitOfWork;
    }

    public override async Task<Guid> ExecuteAsync(UploadUserImageCommand command, CancellationToken ct = new())
    {
        var currentUser = _currentUserProvider.GetCurrentUser();
        if (currentUser is null)
        {
            ThrowError(UserErrors.UserNotFound);
        }

        var user = await _usersRepository
            .GetAsync(new UserByIdSpecification(currentUser.UserId));

        if (user is null)
        {
            ThrowError(UserErrors.UserNotFound);
        }

        var imageFileId = await _filesStore.UploadAsync(command.File);
        user.SetProfileImage(imageFileId);

        _usersRepository.Update(user);
        await _unitOfWork.CommitChangesAsync();

        return imageFileId;
    }
}