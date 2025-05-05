using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Clean.Net;

public class EntityFrameworkRepository<TEntity>(DbContext dbContext, IEventBus eventBus) : IEntityGateway<TEntity> where TEntity : Entity
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
}