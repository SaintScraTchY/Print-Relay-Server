using PrintRelayServer.Domain.Base;

namespace PrintRelayServer.Domain.Entities.Identity;

public class Permission : Entity<Guid>
{
    public string Code { get; set; }   // e.g., "printer.print"
    public string Name { get; set; }
    public string? Description { get; set; }
}

public class ResourceAccessPermission
{
    public Guid ResourceAccessId { get; set; }
    public ResourceAccess ResourceAccess { get; set; }

    public Guid PermissionId { get; set; }
    public Permission Permission { get; set; }
}
