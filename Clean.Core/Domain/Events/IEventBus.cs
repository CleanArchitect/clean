namespace Clean.Core;

public interface IEventBus
{
    Task RaiseEventAsync(params IEvent[] raisedEvents);
}
