namespace Collapsenav.Net.Tool.Data;

public interface IReadRepository<T> : IRepository<T> where T : class, IEntity
{
    /// <summary>
    /// 根据Id查询
    /// </summary>
    Task<T> QueryAsync(object id);
}
public interface IReadRepository<TKey, T> : IReadRepository<T>, IRepository<TKey, T> where T : class, IEntity<TKey>
{
    /// <summary>
    /// 根据Id查询
    /// </summary>
    Task<T> QueryAsync(TKey id);
}