using PrintRelayServer.Domain.Base;
using PrintRelayServer.Domain.Entities.Identity;

namespace PrintRelayServer.Domain.Entities.DeviceAgg;

public class Device : AuditEntity
{
    public string Name { get; set; }
    public string Description { get; set; }

    public string OsIdentifier { get; set; }
    public int? Port { get; set; }
    
    public Guid OwnerId { get; set; }
    public AppUser Owner { get; set; }

    public Guid DeviceTypeId { get; set; }
    public DeviceType DeviceType { get; set; }

    protected Device()
    {
        
    }

    public Device(string name, string description, Guid ownerId, Guid deviceTypeId)
    {
        Name = name;
        Description = description;
        OwnerId = ownerId;
        DeviceTypeId = deviceTypeId;
    }
    
    public void Edit(string name, string description,Guid userId)
    {
        Name = name;
        Description = description;
    }
}