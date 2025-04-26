using Clean.Net;

namespace Example.Domain;

internal sealed class GetExampleUseCase(IEntityGateway<Example> gateway) : IUseCase<GetExampleInput>
{
    public async Task<IOutput> ExecuteAsync(GetExampleInput input) =>
        new GetExampleOutput(await gateway.FindAsync(input.Id));
}