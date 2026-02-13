using PrintRelayServer.Domain.Base;

namespace PrintRelayServer.Domain.Entities.Identity;

public class ResourceAccess : TimestampedEntity
{
    public Guid UserId { get; set; }
    public AppUser User { get; set; }

    public ResourceKind ResourceKind { get; set; }
    public Guid ResourceId { get; set; }

    public ICollection<ResourceAccessPermission> Permissions { get; set; }
    protected ResourceAccess(Guid userGuid, Guid userId,ResourceKind resourceKind, 
        Guid resourceId)
    {
        UserId = userId;
        ResourceKind = resourceKind;
        ResourceId = resourceId;
    }
}
