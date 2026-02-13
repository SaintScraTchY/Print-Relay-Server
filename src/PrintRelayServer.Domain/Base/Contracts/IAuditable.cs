namespace PrintRelayServer.Domain.Base.Contracts;

/// <summary>
/// Contract for auditable entities
/// </summary>
public interface IAuditable
{
    DateTime CreatedAt { get; }
    DateTime? ModifiedAt { get; }
}