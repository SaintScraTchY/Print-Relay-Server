using PrintRelayServer.Application.Application.Interfaces; using PrintRelayServer.Application.Mappers;
using PrintRelayServer.Domain.Entities.DeviceAgg;
using PrintRelayServer.Domain.IRepositories.IDeviceRepo;
using PrintRelayServer.Shared.Contracts.Base;
using PrintRelayServer.Shared.Contracts.Device;
using PrintRelayServer.Shared.Contracts.DeviceType;
using PrintRelayServer.Shared.Contracts.DeviceTypeOption;

namespace PrintRelayServer.Application.Application.Implementations;

public class DeviceApplication : IDeviceApplication
{
    private readonly IDeviceRepository _deviceRepository;
    private readonly IDeviceTypeRepository _deviceTypeRepository;
    private readonly IDeviceTypeOptionRepository _deviceTypeOptionRepository;
    private readonly IDeviceOptionValueRepository _deviceOptionValueRepository;
    private static readonly Guid userId = Guid.NewGuid();

    public DeviceApplication(IDeviceRepository deviceRepository, IDeviceTypeRepository deviceTypeRepository, IDeviceTypeOptionRepository deviceTypeOptionRepository, IDeviceOptionValueRepository deviceOptionValueRepository)
    {
        _deviceRepository = deviceRepository;
        _deviceTypeRepository = deviceTypeRepository;
        _deviceTypeOptionRepository = deviceTypeOptionRepository;
        _deviceOptionValueRepository = deviceOptionValueRepository;
    }

    public async Task<BaseResult<GetDevice?>> EditDevice(Guid deviceId, EditDevice editDevice)
    {
        var device = await _deviceRepository.GetByIdAsync(deviceId);
        if(device == null) return ReturnResult<GetDevice?>.Error(null,"Device not found");
        
        device.Edit(editDevice.Name, editDevice.Code,userId);
        _deviceRepository.Update(device);
        if (await _deviceRepository.SaveChangesAsync())
            return ReturnResult<GetDevice?>.Success(DeviceMapper.MapToGetDevice(device));

        return ReturnResult<GetDevice?>.Error(null, "Device not updated");
    }

    public async Task<BaseResult<PaginatedResult<GetDevice>>> GetDeviceList(int pageNumber, int pageSize)
    {
        var devices = await _deviceRepository.GetPaginatedAsync(pageNumber,pageSize,orderBy:x => x.OrderByDescending(d=>d.Id));
        var result = new PaginatedResult<GetDevice>
        {
            Results = DeviceMapper.MapToGetDevices(devices.Results),
            TotalCount = devices.TotalCount,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
        return ReturnResult<PaginatedResult<GetDevice>>.Success(result);
    }

    public async Task<BaseResult<Guid>> RemoveDevice(Guid deviceId)
    {
        var device = await _deviceRepository.GetByIdAsync(deviceId);
        if (device == null) return ReturnResult<Guid>.Error(deviceId, "Device not found");
        
        device.SoftDelete();
        _deviceRepository.Update(device);
        if(await _deviceRepository.SaveChangesAsync())
            return ReturnResult<Guid>.Success(deviceId);
        
        return ReturnResult<Guid>.Error(deviceId, "Device not Removed");
    }

    public async Task<BaseResult<GetDeviceType>> AddDeviceType(AddDeviceType addDeviceType)
    {
        if (await _deviceTypeRepository.ExistsAsync(x => x.Name == addDeviceType.Name))
            return ReturnResult<GetDeviceType>.Error(null, "Device type already exists");

        var deviceType = await _deviceTypeRepository.AddAsync(new DeviceType(addDeviceType.Name, addDeviceType.Description));
        if (await _deviceTypeRepository.SaveChangesAsync())
            return ReturnResult<GetDeviceType>.Success(DeviceMapper.MapToGetDeviceType(deviceType));
        
        return ReturnResult<GetDeviceType>.Error(null, "Device type not added");
    }

    public async Task<BaseResult<GetDeviceType>> EditDeviceType(Guid deviceId, EditDeviceType editDeviceType)
    {
        var deviceType = await _deviceTypeRepository.GetByIdAsync(deviceId);
        if (deviceType == null)
            return ReturnResult<GetDeviceType>.Error(null, "Device not found");
        
        deviceType.Edit(editDeviceType.Name, editDeviceType.Description);
        _deviceTypeRepository.Update(deviceType);
        
        if (await _deviceTypeRepository.SaveChangesAsync())
            return ReturnResult<GetDeviceType>.Success(DeviceMapper.MapToGetDeviceType(deviceType));
        
        return ReturnResult<GetDeviceType>.Error(null, "Device type not updated");
    }

    public async Task<BaseResult<PaginatedResult<GetDeviceType>>> GetDeviceTypeList(int pageNumber, int pageSize)
    {
        var deviceTypes = await _deviceTypeRepository
            .GetPaginatedAsync(pageNumber, pageSize, orderBy: x => x.OrderByDescending(d => d.Id));

        var result = new PaginatedResult<GetDeviceType>
        {
            Results = DeviceMapper.MapToGetDeviceTypes(deviceTypes.Results),
            TotalCount = deviceTypes.TotalCount,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPages = deviceTypes.TotalPages
        };

        return ReturnResult<PaginatedResult<GetDeviceType>>.Success(result);
    }

    public async Task<BaseResult<Guid>> RemoveDeviceType(Guid deviceTypeId)
    {
        var deviceType = await _deviceTypeRepository.GetByIdAsync(deviceTypeId);
        if (deviceType == null) return ReturnResult<Guid>.Error(deviceTypeId, "Device not found");
        
        _deviceTypeRepository.Remove(deviceType);
        if (await _deviceTypeRepository.SaveChangesAsync())
            return ReturnResult<Guid>.Success(deviceTypeId);
        
        return ReturnResult<Guid>.Error(deviceTypeId, "Device not removed");
    }

    public async Task<BaseResult<GetDeviceTypeOption>> AddDeviceTypeOption(AddDeviceTypeOption addDeviceTypeOption)
    {
        if(await _deviceTypeOptionRepository.ExistsAsync(x=>x.OptionName == addDeviceTypeOption.OptionName))
            return ReturnResult<GetDeviceTypeOption>.Error(null, "Device type Option already exists");
        
        var entity =  await _deviceTypeOptionRepository.AddAsync(new DeviceTypeOption(addDeviceTypeOption.OptionName, addDeviceTypeOption.DeviceTypeId));
        if(await _deviceTypeOptionRepository.SaveChangesAsync())
            return ReturnResult<GetDeviceTypeOption>.Success(DeviceMapper.MapToDeviceTypeOption(entity));
        
        return ReturnResult<GetDeviceTypeOption>.Error(null, "Device type not added");
    }

    public async Task<BaseResult<GetDeviceTypeOption>> EditDeviceTypeOption(Guid deviceTypeOptionId, EditDeviceTypeOption editDeviceTypeOption)
    {
        var deviceTypeOption = await _deviceTypeOptionRepository.GetByIdAsync(deviceTypeOptionId);
        if (deviceTypeOption == null)
            return ReturnResult<GetDeviceTypeOption>.Error(null, "Device type not found");

        deviceTypeOption.Edit(editDeviceTypeOption.Name);
        _deviceTypeOptionRepository.Update(deviceTypeOption);
        
        if(await _deviceTypeOptionRepository.SaveChangesAsync())
            return ReturnResult<GetDeviceTypeOption>.Success(DeviceMapper.MapToDeviceTypeOption(deviceTypeOption));
        
        return ReturnResult<GetDeviceTypeOption>.Error(null, "Device type not updated");
    }

    public async Task<BaseResult<IList<GetDeviceTypeOption>>> GetDeviceTypeOptionList(Guid deviceTypeId)
    {
        var deviceTypeOptions = await _deviceTypeOptionRepository
            .GetAllAsync(orderBy: x => x.OrderByDescending(d => d.Id));

        return ReturnResult<IList<GetDeviceTypeOption>>.Success(DeviceMapper.MapToDeviceTypeOptions(deviceTypeOptions));
    }

    public async Task<BaseResult<Guid>> RemoveDeviceTypeOption(Guid deviceTypeOptionId)
    {
        var deviceTypeOption = await _deviceTypeOptionRepository.GetByIdAsync(deviceTypeOptionId);
        if (deviceTypeOption == null)
            return ReturnResult<Guid>.Error(deviceTypeOptionId, "Device type not found");

        _deviceTypeOptionRepository.Remove(deviceTypeOption);
        
        if(await _deviceTypeOptionRepository.SaveChangesAsync())
            return ReturnResult<Guid>.Success(deviceTypeOptionId);
        
        return ReturnResult<Guid>.Error(deviceTypeOptionId, "Device type not updated");
    }
}