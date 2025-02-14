using PrintRelayServer.Shared.Contracts.Base;
using PrintRelayServer.Shared.Contracts.Device;
using PrintRelayServer.Shared.Contracts.DeviceType;
using PrintRelayServer.Shared.Contracts.DeviceTypeOption;

namespace PrintRelayServer.Application.Application.Interfaces;

public interface IDeviceApplication
{
    #region Device
    Task<BaseResult<GetDevice?>> EditDevice(Guid deviceId, EditDevice editDevice);
    Task<BaseResult<PaginatedResult<GetDevice>>> GetDeviceList(int pageNumber, int pageSize);
    Task<BaseResult<Guid>> RemoveDevice(Guid deviceId);
    #endregion

    #region DeviceType
    Task<BaseResult<GetDeviceType>> AddDeviceType(AddDeviceType addDeviceType);
    Task<BaseResult<GetDeviceType>> EditDeviceType(Guid deviceId, EditDeviceType editDeviceType);
    Task<BaseResult<PaginatedResult<GetDeviceType>>> GetDeviceTypeList(int pageNumber, int pageSize);
    Task<BaseResult<Guid>> RemoveDeviceType(Guid deviceTypeId);
    #endregion
    
    #region DeviceType
    Task<BaseResult<GetDeviceTypeOption>> AddDeviceTypeOption(AddDeviceTypeOption addDeviceTypeOption);
    Task<BaseResult<GetDeviceTypeOption>> EditDeviceTypeOption(Guid deviceTypeOptionId, EditDeviceTypeOption editDeviceTypeOption);
    Task<BaseResult<IList<GetDeviceTypeOption>>> GetDeviceTypeOptionList(Guid deviceTypeOptionId);
    Task<BaseResult<Guid>> RemoveDeviceTypeOption(Guid deviceTypeOptionId);
    #endregion
}