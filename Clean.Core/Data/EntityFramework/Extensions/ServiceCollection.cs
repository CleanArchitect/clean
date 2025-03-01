using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Core;

public static class EntityFrameworkServiceCollectionExtensions
{
    public static IServiceCollection AddCleanEntityFramework<TDbContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> options = null, Type typeEntityGateway = null) where TDbContext : DbContext =>
        services
            .AddDbContext<DbContext, TDbContext>(options)
            .AddEntityGateway(typeEntityGateway ?? typeof(EntityFrameworkRepository<>));

    private static IServiceCollection AddEntityGateway(this IServiceCollection services, Type typeEntityGateway) =>
        typeEntityGateway.Implements(typeof(IEntityGateway<>))
            ? services.AddScoped(typeof(IEntityGateway<>), typeEntityGateway)
            : throw new InvalidOperationException();
}