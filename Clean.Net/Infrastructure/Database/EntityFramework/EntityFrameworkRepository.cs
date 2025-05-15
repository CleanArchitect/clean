using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Clean.Net;

/// <summary>
/// A default generic implementation for service type <see cref="IEntityGateway{TEntity}"/> using Entity Framework.
/// Inherit this class and override functionality if needed and register it using 
/// <see cref="EntityFrameworkServiceCollectionExtensions.AddCleanEntityFramework{TDbContext}(Microsoft.Extensions.DependencyInjection.IServiceCollection, Action{DbContextOptionsBuilder}, Type)"/>. 
/// </summary>
/// <typeparam name="TEntity">Type parameter, must be of type Entity</typeparam>
/// <param name="dbContext">EF</param>
/// <param name="eventBus">Domain Events will be raised when SaveChanges is called.</param>
public class EntityFrameworkRepository<TEntity>(DbContext dbContext, IEventBus eventBus) : IDisposable, IEntityGateway<TEntity> where TEntity : Entity
{
    public virtual async Task<TEntity> FindAsync(params object[] keyValues) =>
        await dbContext
            .Set<TEntity>()
            .FindAsync(keyValues);

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null) =>
        predicate is null
            ? await dbContext
                .Set<TEntity>()
                .ToArrayAsync()
            : await dbContext
                .Set<TEntity>()
                .Where(predicate)
                .ToArrayAsync();

    public virtual async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, bool orDefault = false) =>
        orDefault
            ? await dbContext
                .Set<TEntity>()
                .SingleOrDefaultAsync(predicate)
            : await dbContext
                .Set<TEntity>()
                .SingleAsync(predicate);

    public virtual async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, bool orDefault = false) =>
        orDefault
            ? await dbContext
                .Set<TEntity>()
                .FirstOrDefaultAsync(predicate)
            : await dbContext
                .Set<TEntity>()
                .FirstAsync(predicate);

    public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate) =>
        await dbContext
            .Set<TEntity>()
            .AnyAsync(predicate);

    public virtual IEntityGateway<TEntity> Add(TEntity entity)
    {
        dbContext
            .Set<TEntity>()
            .Add(entity);

        return this;
    }

    public virtual IEntityGateway<TEntity> AddRange(IEnumerable<TEntity> entities)
    {
        dbContext
            .Set<TEntity>()
            .AddRange(entities);

        return this;
    }

    public virtual async Task<IEntityGateway<TEntity>> DeleteAsync(params object[] keyValues)
    {
        var entity = await dbContext
            .Set<TEntity>()
            .FindAsync(keyValues) ?? throw new NullReferenceException($"Entity not found with key(s): '{string.Join(',', keyValues)}'");

        dbContext
            .Set<TEntity>()
            .Remove(entity);

        return this;
    }

    public async Task SaveChangesAsync()
    {
        var raisedEvents = dbContext.ChangeTracker.GetAndClearRaisedEvents();

        await dbContext.SaveChangesAsync();

        await eventBus.RaiseEventAsync(raisedEvents);
    }

    public void Dispose() =>
        dbContext.Dispose();
}