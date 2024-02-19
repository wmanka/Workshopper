using Microsoft.AspNetCore.Http;

namespace Workshopper.Application.Users.Commands.UploadImage;

public record UploadUserImageCommand(IFormFile File) : ICommand<Guid>;