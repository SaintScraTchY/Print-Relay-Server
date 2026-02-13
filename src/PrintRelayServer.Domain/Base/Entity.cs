using PrintRelayServer.Domain.Base.Contracts;

namespace PrintRelayServer.Domain.Base;

public abstract class Entity<TKey> : IEntity<TKey>, IEquatable<Entity<TKey>> 
    where TKey : IEquatable<TKey>
{
    private TKey? _id;
    
    public TKey Id 
    { 
        get => _id ?? throw new InvalidOperationException("Entity not initialized");
        protected set => _id = value;
    }

    // Domain event support for cross-cutting concerns
    private readonly List<DomainEvent> _domainEvents = [];
    public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected Entity() { }

    protected Entity(TKey id)
    {
        if (id.Equals(default))
            throw new ArgumentException("ID cannot be default value", nameof(id));
        
        Id = id;
    }

    public void AddDomainEvent(DomainEvent eventItem)
    {
        _domainEvents.Add(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    // Value-based equality
    public override bool Equals(object? obj) => 
        obj is Entity<TKey> other && Equals(other);

    public bool Equals(Entity<TKey>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        if (GetType() != other.GetType()) return false;
        
        return Id.Equals(other.Id);
    }

    public override int GetHashCode() => Id.GetHashCode();

    public static bool operator ==(Entity<TKey>? left, Entity<TKey>? right) => 
        Equals(left, right);

    public static bool operator !=(Entity<TKey>? left, Entity<TKey>? right) => 
        !Equals(left, right);
}