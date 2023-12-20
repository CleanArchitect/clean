using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Clean.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, Type serviceInterfaceType, Assembly scanAssembly = null, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
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
