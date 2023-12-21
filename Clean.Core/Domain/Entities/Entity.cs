namespace Clean.Core;

public abstract class Entity
{
    protected readonly List<IEvent> events = [];

    public IReadOnlyCollection<IEvent> RaisedEvents => events.AsReadOnly();

    public Guid Id { get; private set; }
}