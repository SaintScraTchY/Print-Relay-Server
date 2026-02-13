namespace PrintRelayServer.Domain.Base;

public record EntitySoftDeletedEvent(Guid EntityId, string EntityType) 
    : DomainEvent(EntityId, EntityType, DateTime.UtcNow);