namespace PrintRelayServer.Domain.Base;

public class SoftDeleteEntity : Entity<Guid>
{
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; } = false;

    public void SoftDelete()
    {
        IsDeleted = true;
    }

    public void DeActivate()
    {
        IsActive = false;
    }

    public void Restore()
    {
        IsDeleted = true;
    }

    public void ReActivate()
    {
        IsActive = true;
    }
}