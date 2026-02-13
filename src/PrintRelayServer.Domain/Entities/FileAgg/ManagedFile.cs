using PrintRelayServer.Domain.Base;

namespace PrintRelayServer.Domain.Entities.FileAgg;

public class ManagedFile : AuditableEntity
{
    public string FileName { get; set; }
    public string Path { get; set; }
    public long Size { get; set; }
    protected ManagedFile(string fileName, string path, long size)
    {
        FileName = fileName;
        Path = path;
        Size = size;
    }
}
