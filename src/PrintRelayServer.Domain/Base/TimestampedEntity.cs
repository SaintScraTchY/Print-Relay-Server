using PrintRelayServer.Domain.Base.Contracts;

namespace PrintRelayServer.Domain.Base;

public abstract class TimestampedEntity : Entity<Guid>,IHasCreatedAt,IHasModifiedAt
{
    public DateTime CreatedAt { get; private set; }
    public DateTime? ModifiedAt { get; private set; }

    protected TimestampedEntity() : base(Guid.NewGuid()) { }

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