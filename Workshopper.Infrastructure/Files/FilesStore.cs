using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Workshopper.Application.Common.Abstractions;

namespace Workshopper.Infrastructure.Files;

public class FilesStore : IFilesStore
{
    private readonly IAmazonS3 _s3;
    private readonly IOptions<FilesStoreOptions> _options;
    private readonly ILogger<FilesStore> _logger;
    private const string FolderName = "files";

    public FilesStore(
        IAmazonS3 s3,
        IOptions<FilesStoreOptions> options,
        ILogger<FilesStore> logger)
    {
        _s3 = s3;
        _options = options;
        _logger = logger;
    }

    public async Task UploadAsync(Guid id, IFormFile file)
    {
        try
        {
            await _s3.PutObjectAsync(new PutObjectRequest
            {
                BucketName = _options.Value.BucketName,
                Key = $"{FolderName}/{id}",
                InputStream = file.OpenReadStream(),
                ContentType = file.ContentType,
                Metadata =
                {
                    ["x-amz-meta-originalname"] = file.FileName,
                    ["x-amz-meta-extension"] = Path.GetExtension(file.FileName)
                }
            });
        }
        catch (AmazonS3Exception e)
        {
            _logger.LogError(e, "Failed to upload file with id {Id} to S3 bucket", id);
            throw new Exception("Failed to upload file to S3 bucket");
        }
    }
    public async Task<FileReponse?> DownloadAsync(Guid id)
    {
        try
        {
            var response = await _s3.GetObjectAsync(new GetObjectRequest
            {
                BucketName = _options.Value.BucketName,
                Key = $"{FolderName}/{id}"
            });

            return new FileReponse(
                response.ResponseStream,
                response.Headers.ContentType,
                response.Metadata["x-amz-meta-originalname"],
                response.Metadata["x-amz-meta-extension"]);
        }
        catch (AmazonS3Exception e)
        {
            _logger.LogError(e, "Failed to download file with id {Id} from S3 bucket", id);
            return null;
        }
    }
    public async Task DeleteAsync(Guid id)
    {
        try
        {
            await _s3.DeleteObjectAsync(new DeleteObjectRequest
            {
                BucketName = _options.Value.BucketName,
                Key = $"{FolderName}/{id}"
            });
        }
        catch (AmazonS3Exception e)
        {
            _logger.LogError(e, "Failed to delete file with id {Id} from S3 bucket", id);
            throw new Exception("Failed to delete file from S3 bucket");
        }
    }
}