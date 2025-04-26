using Clean.Net;

namespace Example.Domain;

public sealed class CreateExampleInput : IInput
{
    public string Name { get; set; }
}