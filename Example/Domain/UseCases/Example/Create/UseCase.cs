using Clean.Net;

namespace Example.Domain;

internal sealed class CreateExampleUseCase(IEntityGateway<Example> gateway) : IUseCase<CreateExampleInput>
{
    public async Task<IOutput> ExecuteAsync(CreateExampleInput input)
    {
        var example = new Example(input);

        await gateway
            .Add(example)
            .SaveChangesAsync();

        return new CreateExampleOutput(example);
    }
}