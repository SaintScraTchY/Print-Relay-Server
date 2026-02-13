namespace PrintRelayServer.Domain.Base.Contracts;

public interface IHasCreatedAt
{
    DateTime CreatedAt { get; }
}