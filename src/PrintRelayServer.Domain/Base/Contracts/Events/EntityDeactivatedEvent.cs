namespace PrintRelayServer.Domain.Base.Contracts.Events;

public record EntityDeactivatedEvent(Guid EntityId, string EntityType) 
    : DomainEvent(EntityId, EntityType, DateTime.UtcNow);