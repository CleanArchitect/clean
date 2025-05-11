using System.Linq.Expressions;

namespace Clean.Net;

/// <summary>
/// Injectable gateway service for a specific type of Entity <see cref="Entity"/>.
/// Contains methods to create, read, update or delete domain entities (CRUD).
/// </summary>
public interface IEntityGateway<TEntity> where TEntity : Entity
{
    IEntityGateway<TEntity> Add(TEntity entity);
    IEntityGateway<TEntity> AddRange(IEnumerable<TEntity> entities);

    Task<TEntity> FindAsync(params object[] keyValues);
    TEntity Find(params object[] keyValues) => FindAsync(keyValues).GetAwaiter().GetResult();

    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate = null);
    IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null) => GetAllAsync(predicate).GetAwaiter().GetResult();

    Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate, bool orDefault = false);
    TEntity Single(Expression<Func<TEntity, bool>> predicate, bool orDefault = false) => SingleAsync(predicate, orDefault).GetAwaiter().GetResult();

    Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate, bool orDefault = false);
    TEntity First(Expression<Func<TEntity, bool>> predicate, bool orDefault = false) => FirstAsync(predicate, orDefault).GetAwaiter().GetResult();

    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);
    bool Any(Expression<Func<TEntity, bool>> predicate) => AnyAsync(predicate).GetAwaiter().GetResult();

    Task<IEntityGateway<TEntity>> DeleteAsync(params object[] keyValues);
    IEntityGateway<TEntity> Delete(params object[] keyValues) => DeleteAsync(keyValues).GetAwaiter().GetResult();

    Task SaveChangesAsync();
    void SaveChanges() => Task.Run(SaveChangesAsync);
}