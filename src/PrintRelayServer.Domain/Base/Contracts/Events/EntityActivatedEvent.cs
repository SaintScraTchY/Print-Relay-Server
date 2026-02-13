namespace PrintRelayServer.Domain.Base;

public record EntityActivatedEvent(Guid EntityId, string EntityType) 
    : DomainEvent(EntityId, EntityType, DateTime.UtcNow);