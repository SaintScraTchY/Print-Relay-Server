namespace PrintRelayServer.Domain.Base;

public abstract record DomainEvent(Guid EntityId, string EntityType, DateTime OccurredOn);