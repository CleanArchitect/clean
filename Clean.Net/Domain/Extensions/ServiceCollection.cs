﻿using Microsoft.Extensions.DependencyInjection;
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
    /// <returns>The modified <see cref="IServiceCollection"/> with registered services.</returns>
    public static IServiceCollection AddCleanDomain(this IServiceCollection services, Assembly domainAssembly = null, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped) =>
        services
            .AddServiceImplementations(typeof(IUseCase<>), domainAssembly ??= Assembly.GetCallingAssembly(), serviceLifetime)
            .AddServiceImplementations(typeof(IEventHandler<>), domainAssembly, serviceLifetime)
            .AddSingleton<IInputHandler, InputHandler>()
            .AddSingleton<IEventBus, EventBus>();
}