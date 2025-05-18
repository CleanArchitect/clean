using Clean.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Example.Infrastructure;

internal static class InfrastructureDatabaseServiceCollectionExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString) =>
        services
            .AddCleanInfrastructure<ExampleDbContext>(options => options
                .UseNpgsql(connectionString)
                .UseLazyLoadingProxies());
}