using Clean.Net;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace Domain.Events;

public class EventBusTests
{
    public class TestEvent : IEvent { }

    [Fact]
    public async Task RaiseEventAsync_ShouldInvokeHandler()
    {
        // arrange
        var eventHandlerMock = Substitute.For<IEventHandler<TestEvent>>();
        var eventBus = CreateEventBus(eventHandlerMock);
        var testEvent = new TestEvent();

        // act
        await eventBus.RaiseEventAsync(testEvent);

        // assert
        await eventHandlerMock
            .Received(1)
            .HandleAsync(testEvent);
    }

    private static IEventBus CreateEventBus(IEventHandler<TestEvent> eventHandlerMock)
    {
        var scopeFactory = Substitute.For<IServiceScopeFactory>();
        var serviceScope = Substitute.For<IServiceScope>();
        var serviceProvider = Substitute.For<IServiceProvider>();

        serviceScope.ServiceProvider.Returns(serviceProvider);
        scopeFactory.CreateAsyncScope().Returns(serviceScope);

        serviceProvider
            .GetService<IEventHandler<TestEvent>>()
            .Returns(eventHandlerMock);

        return new EventBus(scopeFactory);
    }
}