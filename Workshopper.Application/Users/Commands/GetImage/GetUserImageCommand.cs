using Workshopper.Application.Common.Abstractions;

namespace Workshopper.Application.Users.Commands.GetImage;

public record GetUserImageCommand(Guid UserId) : ICommand<FileReponse?>;