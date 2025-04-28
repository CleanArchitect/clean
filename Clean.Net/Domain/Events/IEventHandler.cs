namespace Clean.Net;

/// <summary>
/// Represents a handler for a specific type of domain event. Implementations of
/// this interface will automatically be registered to the ServiceCollection when AddCleanDomain 
/// is used. <see cref="DomainServiceCollectionExtensions"/>
/// </summary>
public interface IEventHandler<in TEvent> where TEvent : class, IEvent
{
    Task HandleAsync(TEvent raisedEvent);
}
