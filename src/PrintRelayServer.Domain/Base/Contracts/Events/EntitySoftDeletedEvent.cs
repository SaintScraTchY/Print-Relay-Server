namespace PrintRelayServer.Domain.Base.Contracts.Events;

public record EntitySoftDeletedEvent(Guid EntityId, string EntityType) 
    : DomainEvent(EntityId, EntityType, DateTime.UtcNow);