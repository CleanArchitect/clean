using Clean.Core;

namespace Example.Domain;

public sealed class GetAllExampleInput : IInput
{
}

public sealed class GetAllExamplesOutput : IOutput
{
    public IEnumerable<ExampleModel> Examples { get; }

    internal GetAllExamplesOutput(IEnumerable<Example> examples)
    {
        Examples = [.. examples.Select(ExampleModel.Create)];
    }
}
