namespace Example.Domain;

internal sealed class ExampleModel(Example example)
{
    public Guid Id => example.Id;

    public string Name => example.Name;

    public static ExampleModel Create(Example example) =>
        example == null ? null : new(example);
}