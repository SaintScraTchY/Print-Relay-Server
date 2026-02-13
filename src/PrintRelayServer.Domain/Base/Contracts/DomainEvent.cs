namespace PrintRelayServer.Domain.Base.Contracts;

public abstract record DomainEvent(Guid EntityId, string EntityType, DateTime OccurredOn);