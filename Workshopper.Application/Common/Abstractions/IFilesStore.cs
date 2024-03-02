using Microsoft.AspNetCore.Http;

namespace Workshopper.Application.Common.Abstractions;

public interface IFilesStore // todo: to nuget
{
    Task<Guid> UploadAsync(IFormFile file);

    Task<FileReponse?> DownloadAsync(Guid fileId);

    Task DeleteAsync(Guid fileId);
}