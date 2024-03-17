namespace Workshopper.Infrastructure.FilesStore;

public record FileReponse(
    Stream FileStream,
    string ContentType,
    string FileName,
    string Extension);