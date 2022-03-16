namespace Collapsenav.Net.Tool.Data;

public interface IReadRepository<T> : IRepository<T> where T : class, IEntity
{
    Task<T> QueryAsync(object id);
}
public interface IReadRepository<TKey, T> : IRepository<TKey, T> where T : class, IEntity<TKey>
{
    Task<T> QueryAsync(TKey id);
}