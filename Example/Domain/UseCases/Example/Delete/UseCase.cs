using Clean.Net;

namespace Example.Domain;

internal sealed class DeleteExampleUseCase(IEntityGateway<Example> gateway) : IUseCase<DeleteExampleInput>
{
    public async Task<IOutput> ExecuteAsync(DeleteExampleInput input)
    {
        await gateway
            .Delete(input.Id)
            .SaveChangesAsync();

        return Output.Empty;
    }
}