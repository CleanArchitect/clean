namespace Example.Domain;

public sealed class ExampleModel
{
    public Guid Id { get; }

    public string Name { get; }

    internal ExampleModel(Example example)
    {
        Id = example.Id;
        Name = example.Name;
    }

    internal static ExampleModel Create(Example example) =>
        example == null ? null : new(example);
}