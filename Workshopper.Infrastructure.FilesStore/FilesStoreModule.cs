using System.Text.Json;
using Amazon.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;

namespace Workshopper.Infrastructure.FilesStore;

public static class FilesStoreModule
{
    public static IServiceCollection AddFilesStore(this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        FilesStoreOptions filesStoreOptions;

        if (!environment.IsDevelopment() && !environment.IsEnvironment("Development.Container"))
        {
            services
                .AddOptionsWithValidateOnStart<FilesStoreOptions>()
                .BindConfiguration(FilesStoreOptions.SectionName);

            services.AddSingleton<IValidateOptions<FilesStoreOptions>, FilesStoreOptionsValidator>();
            filesStoreOptions = new FilesStoreOptions();
            configuration.Bind(FilesStoreOptions.SectionName, filesStoreOptions);
        }
        else
        {
            try
            {
                var filesStoreFilePath = Environment.GetEnvironmentVariable("FILES_STORE_FILE");
                var filesStoreFileContent = File.ReadAllText(filesStoreFilePath!).Trim();
                filesStoreOptions = JsonSerializer.Deserialize<FilesStoreOptions>(filesStoreFileContent)!;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("Invalid files store configuration.", e);
            }
        }

        services.AddSingleton(Options.Create(filesStoreOptions));
        services.AddSingleton<IValidateOptions<FilesStoreOptions>, FilesStoreOptionsValidator>();

        services.AddSingleton<IAmazonS3, AmazonS3Client>(_ =>
        {
            return new AmazonS3Client(
                awsAccessKeyId: filesStoreOptions.AccessKeyId,
                awsSecretAccessKey: filesStoreOptions.SecretAccessKey,
                region: Amazon.RegionEndpoint.GetBySystemName(filesStoreOptions.Region));
        });

        services.AddSingleton<IFilesStore, FilesStore>();

        return services;
    }
}