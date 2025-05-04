using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Clean.Net;

internal sealed class EntityFrameworkRepository<TEntity>(DbContext dbContext, IEventBus eventBus) : IEntityGateway<TEntity> where TEntity : Entity
{
    public async Task<TEntity> FindAsync(params object[] key) =>
        await dbContext
            .Set<TEntity>()
            .FindAsync(key);

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null) =>
        predicate is null
            ? await dbContext
                .Set<TEntity>()
                .ToArrayAsync()
            : await dbContext
                .Set<TEntity>()
                .Where(predicate)
                .ToArrayAsync();

    public async Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, bool orDefault = false) =>
        orDefault
            ? await dbContext
                .Set<TEntity>()
                .SingleOrDefaultAsync(predicate)
            : await dbContext
                .Set<TEntity>()
                .SingleAsync(predicate);

    public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, bool orDefault = false) =>
        orDefault
            ? await dbContext
                .Set<TEntity>()
                .FirstOrDefaultAsync(predicate)
            : await dbContext
                .Set<TEntity>()
                .FirstAsync(predicate);

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate) =>
        await dbContext
            .Set<TEntity>()
            .AnyAsync(predicate);

    public IEntityGateway<TEntity> Add(TEntity entity)
    {
        dbContext
            .Set<TEntity>()
            .Add(entity);

        return this;
    }

    public async Task<IEntityGateway<TEntity>> DeleteAsync(params object[] key)
    {
        var entity = await dbContext
            .Set<TEntity>()
            .FindAsync(key) ?? throw new NullReferenceException($"Entity not found with key: '{key}'");

        dbContext
            .Set<TEntity>()
            .Remove(entity);

        return this;
    }

    public async Task SaveChangesAsync()
    {
        await eventBus.RaiseEventAsync(dbContext.ChangeTracker.GetRaisedEvents());

        await dbContext.SaveChangesAsync();
    }
}
