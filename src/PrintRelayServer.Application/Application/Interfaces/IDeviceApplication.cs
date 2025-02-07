using PrintRelayServer.Shared.Contracts.Base;
using PrintRelayServer.Shared.Contracts.Device;
using PrintRelayServer.Shared.Contracts.DeviceType;
using PrintRelayServer.Shared.Contracts.DeviceTypeOption;

namespace PrintRelayServer.Application.Application.Interfaces;

public interface IDeviceApplication
{
    #region Device
    Task<BaseResult<GetDevice>> EditDevice(Guid deviceId, EditDevice editDevice);
    Task<BaseResult<PaginatedResult<GetDevice>>> GetDeviceList();
    Task<BaseResult<GetDevice>> RemoveDevice(Guid deviceId);
    #endregion

    #region DeviceType
    Task<BaseResult<GetDeviceType>> AddDeviceType(AddDeviceType addDeviceType);
    Task<BaseResult<GetDeviceType>> EditDeviceType(Guid deviceId, EditDeviceType editDeviceType);
    Task<BaseResult<PaginatedResult<GetDeviceType>>> GetDeviceTypeList();
    Task<BaseResult<Guid>> RemoveDeviceType(Guid deviceId);
    #endregion
    
    #region DeviceType
    Task<BaseResult<GetDeviceTypeOption>> AddDeviceTypeOption(AddDeviceTypeOption addDeviceTypeOption);
    Task<BaseResult<GetDeviceTypeOption>> EditDeviceTypeOption(Guid deviceId, EditDeviceType editDeviceType);
    Task<BaseResult<PaginatedResult<GetDeviceTypeOption>>> GetDeviceTypeOptionList(Guid deviceTypeId);
    Task<BaseResult<Guid>> RemoveDeviceTypeOption(Guid deviceId);
    #endregion

}