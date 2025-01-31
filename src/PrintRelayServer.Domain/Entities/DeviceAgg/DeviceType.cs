using PrintRelayServer.Domain.Base;

namespace PrintRelayServer.Domain.Entities.DeviceAgg;

public class DeviceType : Entity<Guid>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public ICollection<DeviceTypeOption> AvailableOptions { get; set; }
}