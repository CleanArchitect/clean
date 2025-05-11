using Microsoft.Extensions.DependencyInjection;

namespace Clean.Net;

/// <summary>
/// Injectable service which handles any <see cref="IInput"/> by finding the corresponding
/// Use Case <see cref="IUseCase{TInput}"/>, executing it and returning the <see cref="IOutput"/>.
/// Supports a couple of default IInput/IOutput interfaces.
/// </summary>
public interface IInputHandler
{
    Task<IOutput> HandleAsync(IInput input);
    Task<ICreatedOutput> HandleAsync(ICreateInput input);
    Task<IFileOutput> HandleAsync(IFileExportInput input);
}

internal sealed class InputHandler(IServiceScopeFactory serviceScopeFactory) : IInputHandler
{
    public async Task<IOutput> HandleAsync(IInput input) =>
        await ExecuteUseCase<IOutput>(input);

    public async Task<ICreatedOutput> HandleAsync(ICreateInput input) =>
        await ExecuteUseCase<ICreatedOutput>(input);

    public async Task<IFileOutput> HandleAsync(IFileExportInput input) =>
        await ExecuteUseCase<IFileOutput>(input);

    private async Task<TOutput> ExecuteUseCase<TOutput>(IInput input) where TOutput : IOutput
    {
        await using var scope = serviceScopeFactory.CreateAsyncScope();

        var useCaseExecutor = Activator
            .CreateInstance(typeof(UseCaseExecutor<>)
                .MakeGenericType(input.GetType()), scope.ServiceProvider) as IUseCaseExecutor;

        return (TOutput)await useCaseExecutor.ExecuteAsync(input);
    }
}