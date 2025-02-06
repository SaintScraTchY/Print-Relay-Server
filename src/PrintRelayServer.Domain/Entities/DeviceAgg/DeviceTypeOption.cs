using PrintRelayServer.Domain.Base;

namespace PrintRelayServer.Domain.Entities.DeviceAgg;

public class DeviceTypeOption : FullEntity
{

    public string OptionName { get; set; }
    public Guid DeviceTypeId { get; set; }
    public DeviceType DeviceType { get; set; }
    public ICollection<DeviceOptionValue>? AllowedOptions { get; set; }

    protected DeviceTypeOption()
    {
        
    }
    
    public DeviceTypeOption(string optionName, Guid deviceTypeId, ICollection<DeviceOptionValue>? allowedOptions = null)
    {
        OptionName = optionName;
        DeviceTypeId = deviceTypeId;
        AllowedOptions = allowedOptions ?? new List<DeviceOptionValue>();
    }
    
    public void Edit(string optionName, Guid deviceTypeId)
    {
        OptionName = optionName;
        DeviceTypeId = deviceTypeId;
    }
}