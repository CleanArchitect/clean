using Microsoft.Extensions.DependencyInjection;

namespace Clean.Net;

internal interface IUseCaseExecutor
{
    Task<IOutput> ExecuteAsync(IInput input);
}

internal sealed class UseCaseExecutor<TInput>(IServiceProvider serviceProvider) : IUseCaseExecutor where TInput : class, IInput
{
    public async Task<IOutput> ExecuteAsync(IInput input) =>
        await serviceProvider
            .GetService<IUseCase<TInput>>()
            .ExecuteAsync((TInput)input);
}