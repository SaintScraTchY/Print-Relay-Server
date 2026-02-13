namespace PrintRelayServer.Domain.Base;

public record EntityDeactivatedEvent(Guid EntityId, string EntityType) 
    : DomainEvent(EntityId, EntityType, DateTime.UtcNow);