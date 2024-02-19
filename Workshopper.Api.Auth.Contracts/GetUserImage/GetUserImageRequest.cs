using FastEndpoints;

namespace Workshopper.Api.Auth.Contracts.GetUserImage;

public record GetUserImageRequest
{
    [BindFrom("user-id")]
    public Guid UserId { get; init; }
}