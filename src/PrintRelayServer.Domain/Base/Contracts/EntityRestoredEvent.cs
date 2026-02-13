namespace PrintRelayServer.Domain.Base;

public record EntityRestoredEvent(Guid EntityId, string EntityType) 
    : DomainEvent(EntityId, EntityType, DateTime.UtcNow);