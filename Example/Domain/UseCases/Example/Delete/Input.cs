using Clean.Core;

namespace Example.Domain;

public sealed class DeleteExampleInput(Guid id) : IInput
{
    public Guid Id => id;
}