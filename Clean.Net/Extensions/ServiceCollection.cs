using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Clean.Net;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers all services that implement the specified interface type within the given assembly.
    /// This method scans the provided or calling assembly for implementations of the specified interface type 
    /// and adds them to the service collection with the specified lifetime.
    /// 
    /// Note: Open generic class types will not be registered, register these manually.
    /// </summary>
    /// <param name="services">The service collection to which the implementations will be added.</param>
    /// <param name="serviceInterfaceType">The interface type to search for implementations.</param>
    /// <param name="scanAssembly">
    /// The assembly to scan for implementations of <paramref name="serviceInterfaceType"/>.
    /// If not provided, the calling assembly will be used.
    /// </param>
    /// <param name="serviceLifetime">
    /// Specifies the service lifetime (Scoped, Singleton, or Transient). Defaults to Scoped.
    /// </param>
    /// <returns>The modified <see cref="IServiceCollection"/> with registered services.</returns>
    public static IServiceCollection AddServiceImplementations(this IServiceCollection services, Type serviceInterfaceType, Assembly scanAssembly = null, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
    {
        scanAssembly ??= Assembly.GetCallingAssembly();

        foreach (var implementationType in scanAssembly.GetImplementations(serviceInterfaceType))
        {
            foreach (var serviceType in implementationType.GetInterfaces())
            {
                services.Add(new ServiceDescriptor(serviceType, implementationType, serviceLifetime));
            }
        }

        return services;
    }
}
