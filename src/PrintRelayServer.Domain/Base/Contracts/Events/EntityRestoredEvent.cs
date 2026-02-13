namespace PrintRelayServer.Domain.Base.Contracts.Events;

public record EntityRestoredEvent(Guid EntityId, string EntityType) 
    : DomainEvent(EntityId, EntityType, DateTime.UtcNow);