namespace PrintRelayServer.Domain.Base.Contracts;

/// <summary>
/// Contract for activatable entities
/// </summary>
public interface IActivatable
{
    bool IsActive { get; }
    void Activate();
    void Deactivate();
}