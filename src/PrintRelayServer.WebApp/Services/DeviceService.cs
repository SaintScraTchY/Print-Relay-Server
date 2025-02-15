namespace PrintRelayServer.WebApp.Services;

public class DeviceService : IDeviceService
{
    private readonly HttpClient _httpClient;

    public DeviceService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<GetDevice>> GetDevicesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<GetDevice>>("api/devices") ?? new();
    }

    public async Task<GetDevice> GetDeviceAsync(Guid id)
    {
        return await _httpClient.GetFromJsonAsync<GetDevice>($"api/devices/{id}");
    }

    public async Task<GetDevice> CreateDeviceAsync(CreateDevice device)
    {
        var response = await _httpClient.PostAsJsonAsync("api/devices", device);
        return await response.Content.ReadFromJsonAsync<GetDevice>();
    }

    public async Task<GetDevice> UpdateDeviceAsync(Guid id, UpdateDevice device)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/devices/{id}", device);
        return await response.Content.ReadFromJsonAsync<GetDevice>();
    }

    public async Task DeleteDeviceAsync(Guid id)
    {
        await _httpClient.DeleteAsync($"api/devices/{id}");
    }
} 