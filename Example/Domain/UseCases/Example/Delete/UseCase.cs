using Clean.Core;

namespace Example.Domain;

internal sealed class DeleteExampleUseCase(IEntityGateway<Example> gateway) : IUseCase<DeleteExampleInput>
{
    public async Task<IOutput> ExecuteAsync(DeleteExampleInput input)
    {
        //var example = await gateway.FindAsync(input.Id);

        await gateway
            .Delete(input.Id)
            .SaveChangesAsync();

        return Output.Empty;
    }
}