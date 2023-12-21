using Microsoft.Extensions.DependencyInjection;

namespace Clean.Core;

internal sealed class EventBus(IServiceScopeFactory serviceScopeFactory) : IEventBus
{
    public async Task RaiseEventAsync(params IEvent[] raisedEvents)
    {
        foreach (var raisedEvent in raisedEvents)
        {
            await RaiseEventAsync(raisedEvent);
        }
    }

    private async Task RaiseEventAsync(IEvent raisedEvent)
    {
        await using var scope = serviceScopeFactory.CreateAsyncScope();

        var handler = Activator
            .CreateInstance(typeof(EventHandler<>)
                .MakeGenericType(raisedEvent.GetType()), scope.ServiceProvider) as IEventHandler;

        await handler.HandleAsync(raisedEvent);
    }
}
