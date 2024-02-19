using Workshopper.Api.Auth.Contracts.UploadUserImage;
using Workshopper.Application.Users.Commands.UploadImage;

namespace Workshopper.Api.Auth.UploadUserImage;

public class UploadUserImageEndpoint : Endpoint<UploadUserImageRequest, UploadUserImageResponse>
{
    public override void Configure()
    {
        Post("/users/{user-id}/images");
        AllowFileUploads();

        Description(b => b
            .ProducesProblemDetails(400, "application/json+problem"));

        Summary(s =>
        {
            s.Summary = "Upload a user photo";
        });
    }

    public override async Task HandleAsync(UploadUserImageRequest request, CancellationToken ct)
    {
        var command = new UploadUserImageCommand(request.ImageFile);

        var userImageId = await command.ExecuteAsync(ct);

        await SendOkAsync(new UploadUserImageResponse(userImageId), ct);
    }
}