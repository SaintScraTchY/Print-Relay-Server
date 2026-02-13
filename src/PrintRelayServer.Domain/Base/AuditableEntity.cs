using PrintRelayServer.Domain.Base.Contracts;
using PrintRelayServer.Domain.Entities.Identity;

namespace PrintRelayServer.Domain.Base;

public abstract class AuditableEntity : TimestampedEntity,IHasModifiedBy,IHasCreatedBy
{
    public Guid CreatedById { get; set; }
    public AppUser CreatedBy { get; set; }
    public Guid? ModifiedById { get; protected set; }
    public AppUser? ModifiedBy { get; protected set; }

    protected AuditableEntity() { }

    internal void MarkCreatedBy(Guid userId) => CreatedById = userId;
    internal void MarkModifiedBy(Guid userId) => ModifiedById = userId;
}
