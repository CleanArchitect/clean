using System.Linq.Expressions;

namespace Clean.Core;

public interface IEntityGateway<TEntity> where TEntity : Entity
{
    Task<TEntity> FindAsync(Guid id);
    TEntity Find(Guid id) => FindAsync(id).Result;

    Task<IEnumerable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate);
    IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate) => WhereAsync(predicate).Result;

    Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, bool orDefault = false);
    TEntity Single(Expression<Func<TEntity, bool>> predicate, bool orDefault = false) => SingleAsync(predicate, orDefault).Result;

    Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, bool orDefault = false);
    TEntity First(Expression<Func<TEntity, bool>> predicate, bool orDefault = false) => FirstAsync(predicate, orDefault).Result;

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
    bool Any(Expression<Func<TEntity, bool>> predicate) => AnyAsync(predicate).Result;

    IEntityGateway<TEntity> Add(TEntity entity);

    IEntityGateway<TEntity> Delete(params Guid[] ids);

    Task SaveChangesAsync();
    void SaveChanges() => Task.Run(SaveChangesAsync);
}
