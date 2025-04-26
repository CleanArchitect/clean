namespace Clean.Net;

public interface IEventBus
{
    Task RaiseEventAsync(params IEvent[] raisedEvents);
}
