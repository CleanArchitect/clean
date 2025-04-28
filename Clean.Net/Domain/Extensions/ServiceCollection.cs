using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Clean.Net;

public static class DomainServiceCollectionExtensions
{
    /// <summary>
    /// Registers all services used within Clean Architecture use cases, validation and domain event handling.
    /// Registers all implementations of <see cref="IValidator{T}"/>
    /// Registers all implementations of <see cref="IUseCase{TInput}"/>
    /// Registers all implementations of <see cref="IEventHandler{TEvent}"/>
    /// Registers the <see cref="IInputHandler"/> and <see cref="IEventBus"/>
    /// </summary>
    /// <param name="services">The ServiceCollection where services will be registered to.</param>
    /// <param name="domainAssembly">Assembly used for scanning for implementations. Defaults to calling Assembly.</param>
    /// <param name="serviceLifetime">Lifetime for all implementations of services. Defaults to Scoped.</param>
    /// <returns></returns>
    public static IServiceCollection AddCleanDomain(this IServiceCollection services, Assembly domainAssembly = null, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped) =>
        services
            .AddServices(typeof(IValidator<>), domainAssembly ??= Assembly.GetCallingAssembly(), serviceLifetime)
            .AddServices(typeof(IUseCase<>), domainAssembly, serviceLifetime)
            .AddServices(typeof(IEventHandler<>), domainAssembly, serviceLifetime)
            .AddScoped<IInputHandler, InputHandler>()
            .AddSingleton<IEventBus, EventBus>();
}
