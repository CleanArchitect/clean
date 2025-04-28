namespace Clean.Net;

/// <summary>
/// Represents a domain event. These events can be raised using the <see cref="IEventBus"/>
/// and will be handled by the corresponding <see cref="IEventHandler{TEvent}"/>. Make sure to 
/// make an implementation for your own domain events.
/// </summary>
public interface IEvent { }
