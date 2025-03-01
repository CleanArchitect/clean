using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Clean.Core;

public static class DomainServiceCollectionExtensions
{
    public static IServiceCollection AddCleanDomain(this IServiceCollection services, Assembly domainAssembly = null, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped) =>
        services
            .AddServices(typeof(IValidator<>), domainAssembly ??= Assembly.GetCallingAssembly(), serviceLifetime)
            .AddServices(typeof(IUseCase<>), domainAssembly, serviceLifetime)
            .AddServices(typeof(IEventHandler<>), domainAssembly, serviceLifetime)
            .AddScoped<IInputHandler, InputHandler>()
            .AddSingleton<IEventBus, EventBus>();
}
