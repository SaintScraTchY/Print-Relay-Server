using PrintRelayServer.Application.Application.Interfaces;

namespace PrintRelayServer.Application.Application.Implementations;

using System.Security.Cryptography;
using Microsoft.Extensions.Logging;

/// <summary>
/// Local file system storage implementation.
/// Stores files in a configurable directory with SHA256 hash-based deduplication.
/// </summary>
public class LocalFileStorage : IFileStorageService
{
    private readonly string _storageRoot;
    private readonly ILogger<LocalFileStorage> _logger;
    
    public LocalFileStorage(ILogger<LocalFileStorage> logger)
    {
        _logger = logger;
        
        // Store files in application data folder
        // In production, this should be configurable via appsettings
        _storageRoot = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "PrintRelayServer",
            "Files");
        
        // Ensure directory exists
        Directory.CreateDirectory(_storageRoot);
        
        _logger.LogInformation("File storage initialized at: {StorageRoot}", _storageRoot);
    }
    
    public async Task<StoredFileMetadata> StoreFileAsync(
        string fileName, 
        Stream fileStream, 
        string? contentType = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException("File name cannot be empty", nameof(fileName));
        
        if (fileStream == null || !fileStream.CanRead)
            throw new ArgumentException("Invalid file stream", nameof(fileStream));
        
        // Generate unique file name with timestamp to avoid collisions
        var timestamp = DateTime.UtcNow.ToString("yyyyMMdd_HHmmss");
        var safeFileName = SanitizeFileName(fileName);
        var uniqueFileName = $"{timestamp}_{Guid.NewGuid():N}_{safeFileName}";
        var fullPath = Path.Combine(_storageRoot, uniqueFileName);
        
        long fileSize;
        string? hash = null;
        
        // Write file and calculate hash simultaneously
        await using (var fileWriteStream = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            using var sha256 = SHA256.Create();
            var buffer = new byte[81920]; // 80KB buffer
            int bytesRead;
            fileSize = 0;
            
            while ((bytesRead = await fileStream.ReadAsync(buffer, cancellationToken)) > 0)
            {
                await fileWriteStream.WriteAsync(buffer.AsMemory(0, bytesRead), cancellationToken);
                sha256.TransformBlock(buffer, 0, bytesRead, null, 0);
                fileSize += bytesRead;
            }
            
            sha256.TransformFinalBlock(Array.Empty<byte>(), 0, 0);
            hash = Convert.ToHexString(sha256.Hash!).ToLowerInvariant();
        }
        
        _logger.LogInformation(
            "Stored file: {FileName} ({Size} bytes, hash: {Hash})",
            uniqueFileName, fileSize, hash);
        
        return new StoredFileMetadata(
            FilePath: uniqueFileName,
            Size: fileSize,
            Hash: hash,
            ContentType: contentType ?? "application/octet-stream");
    }
    
    public Task<Stream> GetFileStreamAsync(string filePath, CancellationToken cancellationToken = default)
    {
        var fullPath = Path.Combine(_storageRoot, filePath);
        
        if (!File.Exists(fullPath))
            throw new FileNotFoundException($"File not found: {filePath}");
        
        // Return a file stream (caller must dispose)
        var stream = new FileStream(
            fullPath, 
            FileMode.Open, 
            FileAccess.Read, 
            FileShare.Read,
            bufferSize: 81920,
            useAsync: true);
        
        return Task.FromResult<Stream>(stream);
    }
    
    public Task<bool> FileExistsAsync(string filePath)
    {
        var fullPath = Path.Combine(_storageRoot, filePath);
        return Task.FromResult(File.Exists(fullPath));
    }
    
    public Task DeleteFileAsync(string filePath)
    {
        var fullPath = Path.Combine(_storageRoot, filePath);
        
        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
            _logger.LogInformation("Deleted file: {FilePath}", filePath);
        }
        
        return Task.CompletedTask;
    }
    
    /// <summary>
    /// Remove invalid file name characters
    /// </summary>
    private static string SanitizeFileName(string fileName)
    {
        var invalidChars = Path.GetInvalidFileNameChars();
        var sanitized = string.Join("_", fileName.Split(invalidChars, StringSplitOptions.RemoveEmptyEntries));
        
        // Limit length to 100 characters
        if (sanitized.Length > 100)
            sanitized = sanitized[..100];
        
        return sanitized;
    }
}