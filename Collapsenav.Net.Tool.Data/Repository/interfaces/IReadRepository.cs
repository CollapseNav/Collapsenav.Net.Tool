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

#region 无泛型约束

public interface INoConstraintsReadRepository<T> : INoConstraintsRepository<T>
{
    /// <summary>
    /// 根据Id查询
    /// </summary>
    Task<T> QueryAsync(object id);
}
public interface INoConstraintsReadRepository<TKey, T> : INoConstraintsReadRepository<T>, INoConstraintsRepository<TKey, T>
{
    /// <summary>
    /// 根据Id查询
    /// </summary>
    Task<T> QueryAsync(TKey id);
}
#endregion
