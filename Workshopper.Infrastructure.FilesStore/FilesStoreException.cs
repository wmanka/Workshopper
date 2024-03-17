namespace Workshopper.Infrastructure.FilesStore;

public class FilesStoreException(string message, Exception? ex = null)
    : Exception(message, ex);