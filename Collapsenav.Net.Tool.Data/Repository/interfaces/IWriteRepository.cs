namespace Collapsenav.Net.Tool.Data;

public interface IWriteRepository<T> : IRepository<T> where T : class, IEntity
{
    Task<T> AddAsync(T entity);
    Task<bool> DeleteAsync(object id, bool isTrue = false);
    Task UpdateAsync(T entity);
}
public interface IWriteRepository<TKey, T> : IRepository<TKey, T> where T : class, IEntity<TKey>
{
    Task<bool> DeleteAsync(TKey id, bool isTrue = false);
}