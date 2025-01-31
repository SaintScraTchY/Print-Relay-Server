using PrintRelayServer.Domain.Base;

namespace PrintRelayServer.Domain.Entities.DeviceAgg;

public class DeviceOptionValue : FullEntity
{
    public string Label { get; set; }
    public string Value { get; set; }
}