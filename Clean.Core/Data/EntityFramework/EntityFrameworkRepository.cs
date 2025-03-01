using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Clean.Core;

internal sealed class EntityFrameworkRepository<TEntity>(DbContext dbContext, IEventBus eventBus) : IEntityGateway<TEntity> where TEntity : Entity
{
    public async Task<TEntity> FindAsync(Guid id) =>
        await dbContext
            .Set<TEntity>()
            .FindAsync(id);

    public async Task<IEnumerable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate) =>
        await dbContext
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
        dbContext.Set<TEntity>().Add(entity);
        return this;
    }

    public IEntityGateway<TEntity> Delete(params Guid[] ids)
    {
        var entities = dbContext
            .Set<TEntity>()
            .Where(entity => ids.Contains(entity.Id));

        dbContext.RemoveRange(entities);
        return this;
    }

    public async Task SaveChangesAsync()
    {
        await eventBus.RaiseEventAsync(dbContext.ChangeTracker.GetRaisedEvents());

        await dbContext.SaveChangesAsync();
    }
}
