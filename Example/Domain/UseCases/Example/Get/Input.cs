using Clean.Net;

namespace Example.Domain;

public sealed class GetExampleInput(Guid id) : IInput
{
    public Guid Id => id;
}