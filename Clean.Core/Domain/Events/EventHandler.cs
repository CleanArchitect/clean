using Microsoft.Extensions.DependencyInjection;

namespace Clean.Core;

internal interface IEventHandler
{
    Task HandleAsync(IEvent raisedEvent);
}

internal sealed class EventHandler<TEvent>(IServiceProvider serviceProvider) : IEventHandler where TEvent : class, IEvent
{
    public async Task HandleAsync(IEvent raisedEvent) =>
        await serviceProvider
            .GetService<IEventHandler<TEvent>>()
            .HandleAsync((TEvent)raisedEvent);
}