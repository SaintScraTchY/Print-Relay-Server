using PrintRelayServer.Shared.Contracts.Permissions;

namespace PrintRelayServer.Shared.Contracts.Roles;

public class GetRole
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    //public IEnumerable<GetPermission> Permissions { get; set; }
}