using Clean.Net;

namespace Example.Domain;

internal sealed class GetAllExampleUseCase(IEntityGateway<Example> gateway) : IUseCase<GetAllExamplesInput>
{
    public async Task<IOutput> ExecuteAsync(GetAllExamplesInput input) =>
        new GetAllExamplesOutput(await gateway.GetAllAsync());
}