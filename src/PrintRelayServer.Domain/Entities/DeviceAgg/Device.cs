using PrintRelayServer.Domain.Base;
using PrintRelayServer.Domain.Entities.AgentAgg;
using PrintRelayServer.Domain.Entities.Identity;
using PrintRelayServer.Domain.Entities.PrintAgg;

namespace PrintRelayServer.Domain.Entities.DeviceAgg;

public class Device : FullAuditableEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; }

    public string OsIdentifier { get; private set; }
    public int? Port { get; private set; }
    
    public Guid OwnerId { get; private set; }
    public AppUser Owner { get; set; }

    public Guid DeviceTypeId { get; private set; }
    public DeviceType DeviceType { get; set; }
    
    // Navigation properties
    public ICollection<AgentDevice> AgentDevices { get; set; } = new List<AgentDevice>();
    public ICollection<PrintJob> PrintJobs { get; set; } = new List<PrintJob>();

    protected Device()
    {
        
    }

    public Device(string name, string description, string osIdentifier, Guid ownerId, Guid deviceTypeId, int? port = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Device name cannot be empty", nameof(name));
        
        Name = name;
        Description = description;
        OsIdentifier = osIdentifier;
        OwnerId = ownerId;
        DeviceTypeId = deviceTypeId;
        Port = port;
    }
    
    public void Edit(string name, string description, Guid userId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Device name cannot be empty", nameof(name));
        
        Name = name;
        Description = description;
    }
    
    public void UpdateConnection(string osIdentifier, int? port, Guid userId)
    {
        OsIdentifier = osIdentifier;
        Port = port;
    }
}