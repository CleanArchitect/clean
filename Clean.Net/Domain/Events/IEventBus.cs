namespace Clean.Net;

/// <summary>
/// Injectable service to raise your own domain events. 
/// </summary>
public interface IEventBus
{
    Task RaiseEventAsync(params IEvent[] raisedEvents);
}
