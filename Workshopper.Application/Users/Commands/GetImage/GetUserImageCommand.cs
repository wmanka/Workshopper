using Workshopper.Infrastructure.FilesStore;

namespace Workshopper.Application.Users.Commands.GetImage;

public record GetUserImageCommand(Guid UserId) : ICommand<FileReponse?>;