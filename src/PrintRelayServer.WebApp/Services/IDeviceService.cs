using PrintRelayServer.Shared.Contracts.Device;

namespace PrintRelayServer.WebApp.Services;

public interface IDeviceService
{
    Task<List<GetDevice>> GetDevicesAsync();
    Task<GetDevice> GetDeviceAsync(Guid id);
    Task<GetDevice> UpdateDeviceAsync(Guid id, EditDevice device);
    Task DeleteDeviceAsync(Guid id);
} 