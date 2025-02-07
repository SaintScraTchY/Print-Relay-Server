using System.ComponentModel.DataAnnotations;

namespace PrintRelayServer.Shared.Contracts.DeviceType;

public class AddDeviceType
{
    [Required]
    public string Name { get; set; }
    public string? Description { get; set; }
}