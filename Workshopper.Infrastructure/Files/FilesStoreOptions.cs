namespace Workshopper.Infrastructure.Files;

public class FilesStoreOptions
{
    public const string SectionName = "FilesStoreSettings";

    public string BucketName { get; init; } = null!;

    public string AccessKeyId { get; init; } = null!;

    public string SecretAccessKey { get; init; } = null!;
}