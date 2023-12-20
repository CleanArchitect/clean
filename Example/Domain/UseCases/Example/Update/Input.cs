using Clean.Core;

namespace Example.Domain;

public sealed class UpdateExampleInput : IInput
{
    public Guid Id { get; private set; }

    public string Name { get; set; }

    public UpdateExampleInput SetId(Guid id)
    {
        Id = id;
        return this;
    }
}