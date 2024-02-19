using FastEndpoints;
using Microsoft.AspNetCore.Http;

namespace Workshopper.Api.Auth.Contracts.UploadUserImage;

public record UploadUserImageRequest
{
    [BindFrom("user-id")]
    public Guid UserId { get; init; }

    public IFormFile ImageFile { get; init; } = null!;
}