namespace PrintRelayServer.Domain.Base.Contracts;

/// <summary>
/// Marker interface for entity identification
/// </summary>
public interface IEntity<TKey> where TKey : IEquatable<TKey>
{
    TKey Id { get; }
}