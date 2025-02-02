using PrintRelayServer.Domain.Base;

namespace PrintRelayServer.Domain.Entities.DeviceAgg;

public class DeviceType : Entity<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<DeviceTypeOption>? AvailableOptions { get; set; }
    public ICollection<Device>? Devices { get; set; }
    
    public DeviceType(string name, string description)
    {
        Name = name;
        Description = description;
    }
    
    public void Edit(string name, string description)
    {
        Name = name;
        Description = description;
    }
}