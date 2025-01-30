namespace PrintRelayServer.Domain.Base;

public class AuditEntity : Entity<Guid>
{
    public Guid CreatedBy { get; set; } 
    public DateTime CreatedOn { get; set; }
    public Guid? UpdatedBy { get; set; }
    public DateTime? UpdatedOn { get; set; }

    public AuditEntity()
    {
        CreatedOn = DateTime.Now;
    }

    public void Updated(Guid userGuid)
    {
        UpdatedOn = DateTime.Now;
        UpdatedBy = userGuid;
    }
}