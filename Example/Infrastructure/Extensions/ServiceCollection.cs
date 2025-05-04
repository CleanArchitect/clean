using Microsoft.Extensions.DependencyInjection;

namespace Example.Infrastructure;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString) =>
        services
            .AddDatabase(connectionString);
}