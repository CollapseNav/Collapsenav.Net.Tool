using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;
public interface IWriteApplication<T> : IApplication<T>, IDisposable
    where T : IEntity
{
    /// <summary>
    /// 删除(单个 id)
    /// </summary>
    Task DeleteAsync(string id, bool isTrue = false);
    /// <summary>
    /// 添加(单个)
    /// </summary>
    // Task<T> AddAsync(T entity);
}
public interface IWriteApplication<TKey, T> : IWriteApplication<T>
    where T : IEntity<TKey>
{
    /// <summary>
    /// 删除(单个 id)
    /// </summary>
    Task DeleteAsync(TKey id, bool isTrue = false);
}

