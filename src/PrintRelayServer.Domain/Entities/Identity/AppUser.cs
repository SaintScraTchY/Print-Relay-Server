using Microsoft.AspNetCore.Identity;
using PrintRelayServer.Domain.Entities.DeviceAgg;

namespace PrintRelayServer.Domain.Entities.Identity;

public class AppUser : IdentityUser<Guid>
{
    public ICollection<Device> Devices { get; set; }
    public ICollection<AppRole> Roles { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    public void EditUser(string? firstName, string? lastName, string? email, string? userName)
    {
        FirstName = firstName ?? FirstName;
        LastName = lastName ?? LastName;
        Email = email ?? Email;
        UserName = userName ?? UserName;
    }
}