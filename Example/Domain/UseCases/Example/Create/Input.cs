using Clean.Core;

namespace Example.Domain;

public sealed class CreateExampleInput : IInput
{
    public string Name { get; set; }
}