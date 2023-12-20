using Microsoft.Extensions.DependencyInjection;

namespace Clean.Core;

public interface IInputHandler
{
    Task<IOutput> HandleAsync<TInput>(TInput input) where TInput : IInput;
}

internal sealed class InputHandler(IServiceProvider serviceProvider) : IInputHandler
{
    public async Task<IOutput> HandleAsync<TInput>(TInput input) where TInput : IInput =>
        await serviceProvider
            .GetService<IUseCase<TInput>>()
            .ExecuteAsync(input);
}
