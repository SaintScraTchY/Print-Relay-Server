using PrintRelayServer.Domain.Base;

namespace PrintRelayServer.Domain.Entities.DeviceAgg;

public class DeviceOptionValue : FullEntity
{
    public string Label { get; set; }
    public string Value { get; set; }

    public Guid DeviceTypeOptionId { get; set; }
    public DeviceTypeOption? DeviceTypeOption { get; set; }

    public DeviceOptionValue(string label, string value)
    {
        Label = label;
        Value = value;
    }
    
    public void Edit(string label, string value)
    {
        Label = label;
        Value = value;
    }
}