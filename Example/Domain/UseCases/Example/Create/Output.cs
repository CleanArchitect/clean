using Clean.Core;

namespace Example.Domain;

internal sealed class CreateExampleOutput(Example example) : IOutput
{
    public Guid? Id => example.Id;

    public ExampleModel Example => ExampleModel.Create(example);
}
