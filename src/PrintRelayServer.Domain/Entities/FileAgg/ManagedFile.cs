using PrintRelayServer.Domain.Base;
using PrintRelayServer.Domain.Entities.PrintAgg;

namespace PrintRelayServer.Domain.Entities.FileAgg;

public class ManagedFile : FullAuditableEntity
{
    public string FileName { get; private set; }
    public string Path { get; private set; }
    public long Size { get; private set; }
    public string? ContentType { get; private set; }
    public string? Hash { get; private set; } // SHA256 hash for deduplication
    
    // Navigation
    public ICollection<PrintJob> PrintJobs { get; set; } = new List<PrintJob>();
    
    protected ManagedFile(Guid userGuid) { }
    
    public ManagedFile(Guid userId, string fileName, string path, long size, string? contentType = null) 
    {
        if (string.IsNullOrWhiteSpace(fileName))
            throw new ArgumentException("File name cannot be empty", nameof(fileName));
        
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("File path cannot be empty", nameof(path));
        
        if (size <= 0)
            throw new ArgumentException("File size must be positive", nameof(size));
        
        FileName = fileName;
        Path = path;
        Size = size;
        ContentType = contentType;
    }
    
    public void SetHash(string hash)
    {
        Hash = hash;
    }
}