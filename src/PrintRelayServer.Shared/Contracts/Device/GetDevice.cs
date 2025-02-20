using PrintRelayServer.Shared.Contracts.DeviceType;
using PrintRelayServer.Shared.Contracts.Users;

namespace PrintRelayServer.Shared.Contracts.Device;

public class GetDevice
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }

    public string OsIdentifier { get; set; }
    
    public GetUser Owner { get; set; }

    public GetDeviceType DeviceType { get; set; }
}