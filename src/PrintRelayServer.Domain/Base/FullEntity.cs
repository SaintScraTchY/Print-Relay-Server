using PrintRelayServer.Domain.Entities.Identity;

namespace PrintRelayServer.Domain.Base;

public class FullEntity : Entity<Guid>
{
    public Guid CreatedBy { get; set; }
    public AppUser? Creator { get; set; }
    public Guid? UpdatedBy { get; private set; }
    public AppUser? Modifier { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public bool IsActive { get; set; } = true;
    public bool IsDeleted { get; set; } = false;

    protected FullEntity()
    {
        CreatedOn = DateTime.Now;
    }

    protected void Updated(Guid userGuid)
    {
        UpdatedOn = DateTime.Now;
        UpdatedBy = userGuid;
    }

    public void SoftDelete(Guid userGuid)
    {
        IsDeleted = true;
        Updated(userGuid);
    }

    public void DeActivate(Guid userGuid)
    {
        IsActive = false;
        Updated(userGuid);
    }

    public void Restore(Guid userGuid)
    {
        IsDeleted = true;
        Updated(userGuid);
    }

    public void ReActivate(Guid userGuid)
    {
        IsActive = true;
        Updated(userGuid);
    }
}