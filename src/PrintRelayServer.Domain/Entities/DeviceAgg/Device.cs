using PrintRelayServer.Domain.Base;
using PrintRelayServer.Domain.Entities.Identity;

namespace PrintRelayServer.Domain.Entities.DeviceAgg;

public class Device : FullEntity
{
    public string Name { get; set; }
    public string Code { get; set; }
    public Guid OwnerId { get; set; }
    public AppUser Owner { get; set; }

    public Guid DeviceTypeId { get; set; }
    public DeviceType DeviceType { get; set; }

    public Device(string name, string code, Guid ownerId, Guid deviceTypeId)
    {
        Name = name;
        Code = code;
        OwnerId = ownerId;
        DeviceTypeId = deviceTypeId;
    }
    
    public void Edit(string name, string code)
    {
        Name = name;
        Code = code;
    }
}