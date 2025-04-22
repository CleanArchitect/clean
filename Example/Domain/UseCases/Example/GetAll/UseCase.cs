using Clean.Core;

namespace Example.Domain;

internal sealed class GetAllExampleUseCase(IEntityGateway<Example> gateway) : IUseCase<GetAllExampleInput>
{
    public async Task<IOutput> ExecuteAsync(GetAllExampleInput input) =>
        new GetAllExamplesOutput(await gateway.GetAllAsync());
}