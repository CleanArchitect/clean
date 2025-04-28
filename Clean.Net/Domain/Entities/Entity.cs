namespace Clean.Net;

/// <summary>
/// Base class for every domain Entity in your Domain Layer.
/// Holds a list of raised domain events which can be used for side effects by state changes in your domain.
/// Your domain classes must inherit from this base class if wanted to be used with <see cref="IEntityGateway{TEntity}"/>.
/// </summary>
public abstract class Entity
{
    protected readonly List<IEvent> events = [];

    public IReadOnlyCollection<IEvent> RaisedEvents => events.AsReadOnly();

    public Guid Id { get; private set; }
}