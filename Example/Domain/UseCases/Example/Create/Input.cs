using Clean.Net;

namespace Example.Domain;

public sealed class CreateExampleInput : ICreateInput
{
    public string Name { get; set; }
}