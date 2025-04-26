using Clean.Net;

namespace Example.Domain;

internal sealed class ExampleCreatedEvent(Example example) : IEvent
{
    public Example Example => example;
}

internal sealed class ExampleCreatedEventHandler : IEventHandler<ExampleCreatedEvent>
{
    public async Task HandleAsync(ExampleCreatedEvent raisedEvent) =>
        await Task.Run(() => Console.WriteLine($"Example toegevoegd met id: {raisedEvent.Example.Id}"));
}