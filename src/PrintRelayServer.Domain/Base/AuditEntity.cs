using PrintRelayServer.Domain.Base.Contracts;

namespace PrintRelayServer.Domain.Base;

public abstract class AuditEntity : Entity<Guid>, IAuditable
{
    public DateTime CreatedAt { get; private set; }
    public DateTime? ModifiedAt { get; private set; }

    protected AuditEntity() : base(Guid.NewGuid()) { }

    // Internal method for interceptor to call - not part of public API
    internal void MarkCreated(DateTime timestamp)
    {
        CreatedAt = timestamp;
    }

    internal void MarkModified(DateTime timestamp)
    {
        ModifiedAt = timestamp;
    }
}