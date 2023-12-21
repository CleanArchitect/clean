using System.Linq.Expressions;

namespace Clean.Core;

public interface IEntityGateway<TEntity> where TEntity : Entity
{
    Task<TEntity> FindAsync(Guid id);

    TEntity Find(Guid id) => FindAsync(id).Result;

    Task<IEnumerable<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate);

    IEnumerable<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate) => FindAllAsync(predicate).Result;

    IEntityGateway<TEntity> Add(TEntity entity);

    IEntityGateway<TEntity> Delete(params Guid[] ids);

    Task SaveChangesAsync();

    void SaveChanges() => Task.Run(SaveChangesAsync);
}
