namespace PrintRelayServer.Application.Application.Interfaces;

/// <summary>
/// Service for storing and retrieving files for print jobs.
/// Initial implementation is local file system, but could be swapped for blob storage later.
/// </summary>
public interface IFileStorageService
{
    /// <summary>
    /// Store a file and return metadata
    /// </summary>
    /// <param name="fileName">Original file name</param>
    /// <param name="fileStream">File content stream</param>
    /// <param name="contentType">MIME type (optional)</param>
    /// <param name="cancellationToken"></param>
    /// <returns>Stored file metadata (path, size, hash)</returns>
    Task<StoredFileMetadata> StoreFileAsync(
        string fileName, 
        Stream fileStream, 
        string? contentType = null,
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Retrieve a file stream for printing
    /// </summary>
    /// <param name="filePath">Path returned from StoreFileAsync</param>
    /// <param name="cancellationToken"></param>
    /// <returns>File stream (caller must dispose)</returns>
    Task<Stream> GetFileStreamAsync(
        string filePath, 
        CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Check if a file exists
    /// </summary>
    Task<bool> FileExistsAsync(string filePath);
    
    /// <summary>
    /// Delete a file (for cleanup)
    /// </summary>
    Task DeleteFileAsync(string filePath);
}

/// <summary>
/// Metadata returned after storing a file
/// </summary>
public record StoredFileMetadata(
    string FilePath,      // Relative path for storage
    long Size,            // File size in bytes
    string? Hash,         // SHA256 hash for deduplication (optional)
    string ContentType    // MIME type
);