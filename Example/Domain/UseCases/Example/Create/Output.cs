using Clean.Net;

namespace Example.Domain;

public sealed class CreateExampleOutput : IOutput
{
    public Guid? Id { get; }

    public ExampleModel Example { get; }

    internal CreateExampleOutput(Example example)
    {
        Id = example.Id;
        Example = ExampleModel.Create(example);
    }
}
