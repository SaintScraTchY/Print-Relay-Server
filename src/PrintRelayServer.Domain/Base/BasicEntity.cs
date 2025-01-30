namespace PrintRelayServer.Domain.Base;

public class BasicEntity : Entity<Guid>
{
    public DateTime? UpdatedOn { get; set; }

    public void Updated()
    {
        UpdatedOn = DateTime.Now;
    }
}