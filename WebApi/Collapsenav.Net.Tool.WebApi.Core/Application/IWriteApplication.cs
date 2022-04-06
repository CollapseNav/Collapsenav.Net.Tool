using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;
public interface IWriteApplication<T> : IApplication<T>, IDisposable
    where T : IEntity
{
    /// <summary>
    /// 删除(单个 id)
    /// </summary>
    Task<bool> DeleteAsync(string id, bool isTrue = false);
    /// <summary>
    /// 添加(单个)
    /// </summary>
    Task<T> AddAsync(T entity);
    /// <summary>
    /// 修改
    /// </summary>
    Task<int> UpdateAsync(string id, T entity);
}
public interface IWriteApplication<TKey, T> : IWriteApplication<T>
    where T : IEntity<TKey>
{
    /// <summary>
    /// 删除(单个 id)
    /// </summary>
    Task<bool> DeleteAsync(TKey id, bool isTrue = false);
}

#region 无泛型约束
public interface INoConstraintsWriteApplication<T> : INoConstraintsApplication<T>, IDisposable
{
    /// <summary>
    /// 删除(单个 id)
    /// </summary>
    Task<bool> DeleteAsync(string id, bool isTrue = false);
    /// <summary>
    /// 添加(单个)
    /// </summary>
    Task<T> AddAsync(T entity);
    /// <summary>
    /// 修改
    /// </summary>
    Task<int> UpdateAsync(string id, T entity);
}
public interface INoConstraintsWriteApplication<TKey, T> : INoConstraintsWriteApplication<T>
{
    /// <summary>
    /// 删除(单个 id)
    /// </summary>
    Task<bool> DeleteAsync(TKey id, bool isTrue = false);
}
#endregion
