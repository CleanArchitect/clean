namespace Clean.Net;

/// <summary>
/// Represents a Clean Architecture Use Case for a specific type of <see cref="IInput"/>. Implementations of
/// this interface will automatically be registered to the ServiceCollection when AddCleanDomain 
/// is used. <see cref="DomainServiceCollectionExtensions"/>
/// </summary>
public interface IUseCase<TInput> where TInput : IInput
{
    Task<IOutput> ExecuteAsync(TInput input);
}
