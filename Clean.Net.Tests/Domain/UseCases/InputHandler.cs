using Clean.Net;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace Domain.UseCases;

public class InputHandlerTests
{
    public class TestInput : IInput { }

    [Fact]
    public async Task ExecuteUseCase_ShouldFindCorrectUseCase()
    {
        // arrange
        var serviceProvider = new ServiceProviderBuilder()
            .Register(Substitute.For<IUseCase<TestInput>>())
            .ServiceProvider;

        var handler = CreateHandler(serviceProvider);

        // act
        var result = await handler.HandleAsync(new TestInput());

        // assert
        Assert.NotNull(result);
    }

    [Fact]
    public async Task ExecuteUseCase_ShouldThrowException_WhenNoUseCaseFound()
    {
        // arrange
        var handler = CreateHandler();

        // act & assert
        await Assert.ThrowsAsync<NullReferenceException>(() => handler.HandleAsync(new TestInput()));
    }

    private static InputHandler CreateHandler(IServiceProvider serviceProvider = null)
    {
        var subScopeFactory = Substitute.For<IServiceScopeFactory>();
        var subServiceScope = Substitute.For<IServiceScope>();

        subScopeFactory
            .CreateAsyncScope()
            .Returns(subServiceScope);

        subServiceScope
            .ServiceProvider
            .Returns(serviceProvider ?? Substitute.For<IServiceProvider>());

        return new InputHandler(subScopeFactory);
    }

    private class ServiceProviderBuilder
    {
        private readonly IServiceCollection serviceCollection = new ServiceCollection();

        public IServiceProvider ServiceProvider =>
            serviceCollection.BuildServiceProvider();

        public ServiceProviderBuilder Register<TUseCase>(TUseCase useCase) where TUseCase : class
        {
            serviceCollection.AddScoped(_ => useCase);
            return this;
        }
    }
}