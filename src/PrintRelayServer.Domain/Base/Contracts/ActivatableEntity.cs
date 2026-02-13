namespace PrintRelayServer.Domain.Base.Contracts;

public class ActivatableEntity : TimestampedEntity , IActivatable
{
    public bool IsActive { get; protected set; }
    public void Activate() => IsActive = true;

    public void Deactivate() => IsActive = false;
    public void Toggle() => IsActive = !IsActive;
}