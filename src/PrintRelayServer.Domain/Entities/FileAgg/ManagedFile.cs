using PrintRelayServer.Domain.Base;

namespace PrintRelayServer.Domain.Entities.FileAgg;

public class ManagedFile : FullEntity
{
    public string FileName { get; set; }
    public string Path { get; set; }
    public long Size { get; set; }
}
