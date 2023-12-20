using Clean.Core;

namespace Example.Domain;

internal sealed class GetExampleOutput(Example example) : IOutput
{
    public ExampleModel Example => ExampleModel.Create(example);
}