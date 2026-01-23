using PrintRelayServer.Domain.Base;
using PrintRelayServer.Domain.Entities.Identity;

namespace PrintRelayServer.Domain.Entities.ManagementAgg;

public class TeamPermissionGrant : Entity<Guid>
{
    public Guid TeamId { get; set; }
    public Team Team { get; set; }

    public Guid PermissionId { get; set; }
    public Permission Permission { get; set; }

    // Optionally, a Resource Identifier - this is KEY
    public Guid? ResourceId { get; set; } // e.g., DeviceId, PrinterId
    public string? ResourceType { get; set; } // e.g., "Device", "Printer"

    //Added so we can have scope with resource.  This is important because its not always necessary for every permission
    public string? Scope { get; set; }  //e.g., "DepartmentA", "ClientXYZ"
}

public enum ResourceAccessType
{
    Receive = 1,
    Send,
    View,
    Manage,
}