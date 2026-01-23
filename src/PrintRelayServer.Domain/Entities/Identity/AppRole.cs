using Microsoft.AspNetCore.Identity;

namespace PrintRelayServer.Domain.Entities.Identity;

public class AppRole : IdentityRole<Guid>
{
    public ICollection<Permission> Permissions { get; set; } = new HashSet<Permission>();
}