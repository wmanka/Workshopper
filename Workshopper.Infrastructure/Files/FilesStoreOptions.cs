namespace Workshopper.Infrastructure.Files;

public class FilesStoreOptions
{
    public const string SectionName = "FilesStore";

    public string BucketName { get; init; } = null!;

    public string AccessKeyId { get; init; } = null!;

    public string SecretAccessKey { get; init; } = null!;

    public string Region { get; init; } = null!;
}