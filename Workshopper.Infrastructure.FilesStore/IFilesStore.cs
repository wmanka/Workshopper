using Microsoft.AspNetCore.Http;

namespace Workshopper.Infrastructure.FilesStore;

public interface IFilesStore
{
    Task<Guid> UploadAsync(IFormFile file);

    Task<FileReponse?> DownloadAsync(Guid fileId);

    Task DeleteAsync(Guid fileId);
}