using Microsoft.AspNetCore.Http;

namespace Workshopper.Application.Common.Abstractions;

public interface IFilesStore // todo: to nuget
{
    Task UploadAsync(Guid id, IFormFile file);

    Task<FileReponse?> DownloadAsync(Guid id);

    Task DeleteAsync(Guid id);
}