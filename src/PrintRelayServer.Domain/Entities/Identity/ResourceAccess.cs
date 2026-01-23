using PrintRelayServer.Domain.Base;

namespace PrintRelayServer.Domain.Entities.Identity;

public class ResourceAccess : FullEntity
{
    public Guid UserId { get; set; }
    public AppUser User { get; set; }

    public ResourceKind ResourceKind { get; set; }
    public Guid ResourceId { get; set; }

    public ICollection<ResourceAccessPermission> Permissions { get; set; }
}