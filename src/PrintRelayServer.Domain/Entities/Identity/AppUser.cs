using Microsoft.AspNetCore.Identity;
using PrintRelayServer.Domain.Entities.DeviceAgg;

namespace PrintRelayServer.Domain.Entities.Identity;

public class AppUser : IdentityUser<Guid>
{
    public ICollection<Device> Devices { get; set; }
    
}