namespace PrintRelayServer.Domain.Base.Contracts;

/// <summary>
/// Contract for soft-delete capability
/// </summary>
public interface ISoftDeletable
{
    bool IsDeleted { get; }
    DateTime? DeletedAt { get; }
    void SoftDelete();
    void Restore();
}