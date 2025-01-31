using PrintRelayServer.Domain.Base;

namespace PrintRelayServer.Domain.Entities.DeviceAgg;

public class DeviceTypeOption : FullEntity
{
    public string OptionName { get; set; }
    public ICollection<DeviceOptionValue> AllowedOptions { get; set; }
}