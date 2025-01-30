namespace PrintRelayServer.Domain.Base;

public class FullEntity : Entity<Guid>
{
    public Guid CreatedBy { get; set; } 
    public Guid? UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; } = false;

    public FullEntity()
    {
        CreatedOn = DateTime.Now;
    }
    
    public void Updated(Guid userGuid)
    {
        UpdatedOn = DateTime.Now;
        UpdatedBy = userGuid;
    }

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