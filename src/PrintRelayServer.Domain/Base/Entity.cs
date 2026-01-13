namespace PrintRelayServer.Domain.Base;

public class Entity<TKey>
{
    public TKey Id { get; set; }
    public DateTime CreatedOn { get; set; }

    protected Entity()
    {
        CreatedOn = DateTime.Now;
    }
}