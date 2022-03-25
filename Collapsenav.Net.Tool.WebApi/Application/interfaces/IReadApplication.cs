using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;
public interface IReadApplication<T> : IApplication<T>
    where T : IEntity
{
    /// <summary>
    /// 查找(单个 id)
    /// </summary>
    Task<T> QueryByStringIdAsync(string id);
}
public interface IReadApplication<TKey, T> : IReadApplication<T>
    where T : IEntity<TKey>
{
    /// <summary>
    /// 查找(单个 id)
    /// </summary>
    Task<T> QueryAsync(TKey id);
}

#region 无泛型约束
public interface INoConstraintsReadApplication<T> : INoConstraintsApplication<T>
{
    /// <summary>
    /// 查找(单个 id)
    /// </summary>
    Task<T> QueryByStringIdAsync(string id);
}
public interface INoConstraintsReadApplication<TKey, T> : INoConstraintsReadApplication<T>
{
    /// <summary>
    /// 查找(单个 id)
    /// </summary>
    Task<T> QueryAsync(TKey id);
}
#endregion
