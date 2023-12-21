using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Clean.Core;

internal sealed class EntityFrameworkRepository<TEntity>(DbContext dbContext, IEventBus eventBus) : IEntityGateway<TEntity> where TEntity : Entity
{
    public async Task<TEntity> FindAsync(Guid id) =>
        await dbContext
            .Set<TEntity>()
            .FindAsync(id);

    public async Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate) =>
        await dbContext
            .Set<TEntity>()
            .Where(predicate)
            .ToArrayAsync();

    public IEntityGateway<TEntity> Add(TEntity entity)
    {
        dbContext.Set<TEntity>().Add(entity);
        return this;
    }

    public IEntityGateway<TEntity> Delete(params Guid[] ids)
    {
        dbContext.RemoveRange(ids);
        return this;
    }

    public async Task SaveChangesAsync()
    {
        await eventBus.RaiseEventAsync(dbContext.ChangeTracker.GetRaisedEvents());

        await dbContext.SaveChangesAsync();
    }
}
