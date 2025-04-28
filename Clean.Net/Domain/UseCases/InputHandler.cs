using Microsoft.Extensions.DependencyInjection;

namespace Clean.Net;

/// <summary>
/// Injectable service which handles any <see cref="IInput"/> by finding the corresponding
/// use case <see cref="IUseCase{TInput}"/>, executing it and returning the <see cref="IOutput"/>.
/// </summary>
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
