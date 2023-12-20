using Clean.Core;

namespace Example.Domain;

public sealed class GetExampleInput(Guid id) : IInput
{
    public Guid Id => id;
}