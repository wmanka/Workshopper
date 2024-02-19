using Workshopper.Api.Auth.Contracts.GetUserImage;
using Workshopper.Application.Users.Commands.GetImage;

namespace Workshopper.Api.Auth.GetUserImage;

public class GetUserImageEndpoint : Endpoint<GetUserImageRequest>
{
    public override void Configure()
    {
        Get("/users/{user-id}/images");
        // Description(b => b
        //     .ProducesProblemDetails(400, "application/json+problem"));

        Summary(s =>
        {
            s.Summary = "Get user photo";
            s.ExampleRequest = new GetUserImageRequest
            {
                UserId = Guid.NewGuid()
            };
        });
    }

    public override async Task HandleAsync(GetUserImageRequest request, CancellationToken ct)
    {
        var command = new GetUserImageCommand(request.UserId);

        var response = await command.ExecuteAsync(ct);
        if (response is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendStreamAsync(
            stream: response.FileStream,
            fileName: response.FileName,
            fileLengthBytes: response.FileStream.Length,
            contentType: response.ContentType,
            cancellation: ct);
    }
}