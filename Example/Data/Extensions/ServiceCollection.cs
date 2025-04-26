using Clean.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Example.Data;

public static class DataServiceCollectionExtensions
{
    public static IServiceCollection AddData(this IServiceCollection services, string connectionString) =>
        services
            .AddCleanEntityFramework<ExampleDbContext>(options => options
                .UseNpgsql(connectionString)
                .UseLazyLoadingProxies());
}