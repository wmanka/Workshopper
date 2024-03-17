using System.Net;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Workshopper.Infrastructure.FilesStore;

public class FilesStore : IFilesStore
{
    private readonly IAmazonS3 _s3;
    private readonly IOptions<FilesStoreOptions> _options;
    private readonly ILogger<FilesStore> _logger;

    private const string FolderName = "files";
    private const string FileNameMetadataKey = "x-amz-meta-originalname";
    private const string FileExtensionMetadataKey = "x-amz-meta-extension";

    public FilesStore(
        IAmazonS3 s3,
        IOptions<FilesStoreOptions> options,
        ILogger<FilesStore> logger)
    {
        _s3 = s3;
        _options = options;
        _logger = logger;
    }

    public async Task<Guid> UploadAsync(IFormFile file)
    {
        try
        {
            var fileId = Guid.NewGuid();

            await _s3.PutObjectAsync(new PutObjectRequest
            {
                BucketName = _options.Value.BucketName,
                Key = $"{FolderName}/{fileId}",
                InputStream = file.OpenReadStream(),
                ContentType = file.ContentType,
                Metadata =
                {
                    [FileNameMetadataKey] = file.FileName,
                    [FileExtensionMetadataKey] = Path.GetExtension(file.FileName)
                }
            });

            return fileId;
        }
        catch (AmazonS3Exception ex)
        {
            throw new FilesStoreException("An error occurred while uploading file to S3 bucket", ex);
        }
    }

    public async Task<FileReponse?> DownloadAsync(Guid fileId)
    {
        try
        {
            var response = await _s3.GetObjectAsync(new GetObjectRequest
            {
                BucketName = _options.Value.BucketName,
                Key = $"{FolderName}/{fileId}"
            });

            return new FileReponse(
                response.ResponseStream,
                response.Headers.ContentType,
                response.Metadata[FileNameMetadataKey],
                response.Metadata[FileExtensionMetadataKey]);
        }
        catch (AmazonS3Exception ex) when (ex.Message is "The specified key does not exist")
        {
            _logger.LogError(ex, "File with id {id} does not exsist", fileId);
            return null;
        }
    }

    public async Task DeleteAsync(Guid fileId)
    {
        try
        {
            var response = await _s3.DeleteObjectAsync(new DeleteObjectRequest
            {
                BucketName = _options.Value.BucketName,
                Key = $"{FolderName}/{fileId}"
            });

            var success = response.HttpStatusCode == HttpStatusCode.OK;
            if (!success)
            {
                throw new FilesStoreException($"An error occurred while deleting file with id {fileId} from S3 bucket");
            }
        }
        catch (AmazonS3Exception ex)
        {
            throw new FilesStoreException($"An error occurred while deleting file with id {fileId} from S3 bucket", ex);
        }
    }
}