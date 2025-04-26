namespace Clean.Net;

public interface IUseCase<TInput> where TInput : IInput
{
    Task<IOutput> ExecuteAsync(TInput input);
}
