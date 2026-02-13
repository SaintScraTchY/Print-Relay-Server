using PrintRelayServer.Domain.Base.Contracts;
using PrintRelayServer.Domain.Base.Contracts.Events;
using PrintRelayServer.Domain.Entities.Identity;

namespace PrintRelayServer.Domain.Base;

public abstract class FullAuditableEntity : TimestampedEntity, 
    IHasCreatedBy,
    IHasModifiedBy,
    ISoftDeletable, 
    IActivatable
{
    public Guid CreatedById { get; set; }
    public AppUser CreatedBy { get; set; }
    public Guid? ModifiedById { get; set; }
    public AppUser? ModifiedBy { get; set; }
    
    public bool IsActive { get; private set; } = true;
    public bool IsDeleted { get; private set; }
    public DateTime? DeletedAt { get; private set; }

    protected FullAuditableEntity() { }

    // Factory method pattern for controlled creation
    public static TEntity Create<TEntity>(Guid createdById, Func<TEntity> factory) 
        where TEntity : AuditableEntity
    {
        var entity = factory();
        entity.CreatedById = createdById;
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

    public void Toggle()
    {
        if(IsDeleted)
            throw new InvalidOperationException("Cannot activate a deleted entity");
        
        IsActive = !IsActive;
        //TODO
        //AddDomainEvent(new EntityToggledEvent(Id, GetType().Name, IsActive));
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
        ModifiedById = userId;
    }
}