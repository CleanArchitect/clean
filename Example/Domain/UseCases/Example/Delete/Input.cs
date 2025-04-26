using Clean.Net;

namespace Example.Domain;

public sealed class DeleteExampleInput(Guid id) : IInput
{
    public Guid Id => id;
}