using System.Linq.Expressions;

namespace Clean.Core;

public interface IEntityGateway<TEntity> where TEntity : Entity
{
    IEntityGateway<TEntity> Add(TEntity entity);

    Task<TEntity> FindAsync(Guid id);
    TEntity Find(Guid id) => FindAsync(id).Result;

    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null);
    IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null) => GetAllAsync(predicate).Result;

    Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, bool orDefault = false);
    TEntity Single(Expression<Func<TEntity, bool>> predicate, bool orDefault = false) => SingleAsync(predicate, orDefault).Result;

    Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, bool orDefault = false);
    TEntity First(Expression<Func<TEntity, bool>> predicate, bool orDefault = false) => FirstAsync(predicate, orDefault).Result;

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
    bool Any(Expression<Func<TEntity, bool>> predicate) => AnyAsync(predicate).Result;

    IEntityGateway<TEntity> Delete(IEnumerable<object> keys);
    IEntityGateway<TEntity> Delete(object key) => Delete([key]);

    Task SaveChangesAsync();
    void SaveChanges() => Task.Run(SaveChangesAsync);
}
