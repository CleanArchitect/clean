using Clean.Net;

namespace Example.Domain;

internal class Example : Entity
{
    public string Name { get; private set; }

    protected Example() { }

    public Example(CreateExampleInput input)
    {
        Name = input.Name;

        events.Add(new ExampleCreatedEvent(this));
    }

    public void Update(UpdateExampleInput input)
    {
        Name = input.Name;
    }
}
