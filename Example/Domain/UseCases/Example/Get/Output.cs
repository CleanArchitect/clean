using Clean.Core;

namespace Example.Domain;

public sealed class GetExampleOutput : IOutput
{
    public ExampleModel Example { get; }

    internal GetExampleOutput(Example example)
    {
        Example = ExampleModel.Create(example);
    }
}