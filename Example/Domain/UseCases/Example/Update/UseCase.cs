using Clean.Core;

namespace Example.Domain;

internal sealed class UpdateExampleUseCase(IEntityGateway<Example> gateway) : IUseCase<UpdateExampleInput>
{
    public async Task<IOutput> ExecuteAsync(UpdateExampleInput input)
    {
        var example = await gateway.FindAsync(input.Id);

        example.Update(input);

        await gateway.SaveChangesAsync();

        return Output.Empty;
    }
}