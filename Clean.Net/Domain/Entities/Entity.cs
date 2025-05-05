namespace Clean.Net;

/// <summary>
/// Base class for DDD Domain Entity in your Domain Layer. Holds a list of 
/// raised domain events which can be used for side-effects by state changes in your domain.
/// Your domain classes must inherit from this base class 
/// if you want to use them with <see cref="IEntityGateway{TEntity}"/>.
/// </summary>
public abstract class Entity
{
    protected readonly List<IEvent> events = [];

    public IReadOnlyCollection<IEvent> RaisedEvents => events.AsReadOnly();

    public Guid Id { get; private set; }

    internal void ClearEvents() => events.Clear();
}