using PrintRelayServer.Domain.Base.Contracts;
using PrintRelayServer.Domain.Entities.Identity;

namespace PrintRelayServer.Domain.Base;

public abstract class FullAuditEntity : AuditEntity, 
    IUserTracked, 
    ISoftDeletable, 
    IActivatable
{
    public Guid CreatedBy { get; private set; }
    public Guid? ModifiedBy { get; private set; }
    
    public bool IsActive { get; private set; } = true;
    public bool IsDeleted { get; private set; }
    public DateTime? DeletedAt { get; private set; }

    // Navigation properties (optional, for EF Core)
    public virtual AppUser? Creator { get; set; }
    public virtual AppUser? Modifier { get; set; }

    protected FullAuditEntity() { }

    // Factory method pattern for controlled creation
    public static TEntity Create<TEntity>(Guid createdBy, Func<TEntity> factory) 
        where TEntity : FullAuditEntity
    {
        var entity = factory();
        entity.CreatedBy = createdBy;
        return entity;
    }

    // Domain methods with business logic
    public void Activate()
    {
        if (IsDeleted)
            throw new InvalidOperationException("Cannot activate a deleted entity");
        
        IsActive = true;
        AddDomainEvent(new EntityActivatedEvent(Id, GetType().Name));
    }

    public void Deactivate()
    {
        IsActive = false;
        AddDomainEvent(new EntityDeactivatedEvent(Id, GetType().Name));
    }

    public void SoftDelete()
    {
        if (IsDeleted) return;
        
        IsDeleted = true;
        DeletedAt = DateTime.UtcNow;
        IsActive = false;
        AddDomainEvent(new EntitySoftDeletedEvent(Id, GetType().Name));
    }

    public void Restore()
    {
        if (!IsDeleted) return;
        
        IsDeleted = false;
        DeletedAt = null;
        IsActive = true;
        AddDomainEvent(new EntityRestoredEvent(Id, GetType().Name));
    }

    // Internal modifier for interceptors
    internal void MarkModifiedBy(Guid userId)
    {
        ModifiedBy = userId;
    }
}