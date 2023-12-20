namespace Clean.Core;

public interface IUseCase<TInput> where TInput : IInput
{
    Task<IOutput> ExecuteAsync(TInput input);
}
