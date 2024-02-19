using FluentValidation;
using Workshopper.Api.Auth.Contracts.UploadUserImage;

namespace Workshopper.Api.Auth.UploadUserImage;

public class UploadUserImageValidator : Validator<UploadUserImageRequest>
{
    public UploadUserImageValidator()
    {
        RuleFor(x => x.UserId)
            .NotNull();

        RuleFor(x => x.ImageFile)
            .NotEmpty();
    }
}