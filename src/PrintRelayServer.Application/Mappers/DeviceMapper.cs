using PrintRelayServer.Domain.Entities.DeviceAgg;
using PrintRelayServer.Shared.Contracts.Device;
using PrintRelayServer.Shared.Contracts.DeviceType;
using PrintRelayServer.Shared.Contracts.DeviceTypeOption;
using Riok.Mapperly.Abstractions;

namespace PrintRelayServer.Application.Mappers;

[Mapper]
public static partial class DeviceMapper
{
    #region Device

    public static partial GetDevice MapToGetDevice(Device source);
    public static partial IList<GetDevice> MapToGetDevices(IList<Device> sources);

    #endregion

    #region DeviceType

    public static partial GetDeviceType MapToGetDeviceType(DeviceType source);
    public static partial IList<GetDeviceType> MapToGetDeviceTypes(IList<DeviceType> source);

    #endregion
    
    #region DeviceTypeOption
    public static partial GetDeviceTypeOption MapToDeviceTypeOption(DeviceTypeOption source);
    public static partial IList<GetDeviceTypeOption> MapToDeviceTypeOptions(IEnumerable<DeviceTypeOption> source);
    #endregion 
}