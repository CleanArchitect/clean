using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Clean.Core;

public static class EntityFrameworkServiceCollectionExtensions
{
    public static IServiceCollection AddCleanEntityFramework<TDbContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> options = null) where TDbContext : DbContext =>
        services
            .AddScoped(typeof(IEntityGateway<>), typeof(EntityFrameworkRepository<>))
            .AddDbContext<DbContext, TDbContext>(options);
}