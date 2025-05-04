using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Net;

public static class EntityFrameworkServiceCollectionExtensions
{
    /// <summary>
    /// Configures and registers Entity Framework DbContext along with an Entity Gateway <see cref="IEntityGateway{TEntity}"/> in the service collection.
    /// </summary>
    /// <typeparam name="TDbContext">The type of the Entity Framework database context to be registered.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> where the services will be registered.</param>
    /// <param name="options">
    /// An optional action to configure <see cref="DbContextOptionsBuilder"/> for the database context.
    /// If not provided, the context will be registered with default options.
    /// </param>
    /// <param name="typeEntityGateway">
    /// An optional Type specifying the entity gateway to be registered. Must implement <see cref="IEntityGateway{TEntity}"/>.
    /// If not provided a default Entity Framework gateway will be used.
    /// </param>
    /// <returns>The <see cref="IServiceCollection"/> with registered DbContext and Entity Gateway</returns>
    public static IServiceCollection AddCleanEntityFramework<TDbContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> options = null, Type typeEntityGateway = null) where TDbContext : DbContext =>
        services
            .AddDbContext<DbContext, TDbContext>(options)
            .AddEntityGateway(typeEntityGateway ?? typeof(EntityFrameworkRepository<>));

    private static IServiceCollection AddEntityGateway(this IServiceCollection services, Type typeEntityGateway) =>
        typeEntityGateway.Implements(typeof(IEntityGateway<>))
            ? services.AddScoped(typeof(IEntityGateway<>), typeEntityGateway)
            : throw new InvalidOperationException($"Invalid Type:'{typeEntityGateway}' must implement IEntityGateway");
}