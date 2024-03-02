namespace Workshopper.Infrastructure.Files;

public class FilesStoreException(string message, Exception? ex = null)
    : Exception(message, ex);