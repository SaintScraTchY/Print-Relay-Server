namespace PrintRelayServer.Domain.Base.Contracts.Events;

public record EntityActivatedEvent(Guid EntityId, string EntityType) 
    : DomainEvent(EntityId, EntityType, DateTime.UtcNow);