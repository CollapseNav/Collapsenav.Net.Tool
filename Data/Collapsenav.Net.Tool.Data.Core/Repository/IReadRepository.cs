namespace Collapsenav.Net.Tool.Data;

public interface IReadRepository<T> : IRepository<T>, ICountRepository<T>, ICheckExistRepository<T> where T : class, IEntity
{
    /// <summary>
    /// 根据Id查询
    /// </summary>
    Task<T> GetByIdAsync(object id);
    Task<IEnumerable<T>> QueryDataAsync(IQueryable<T> query);
}
public interface IReadRepository<TKey, T> : IReadRepository<T>, IRepository<TKey, T> where T : class, IEntity<TKey>
{
    /// <summary>
    /// 根据Id查询
    /// </summary>
    Task<T> GetByIdAsync(TKey id);
}

#region 无泛型约束

public interface INoConstraintsReadRepository<T> : INoConstraintsRepository<T>, INoConstraintsCountRepository<T>, INoConstraintsCheckExistRepository<T>
{
    /// <summary>
    /// 根据Id查询
    /// </summary>
    Task<T> GetByIdAsync(object id);
    Task<IEnumerable<T>> QueryDataAsync(IQueryable<T> query);
}
public interface INoConstraintsReadRepository<TKey, T> : INoConstraintsReadRepository<T>, INoConstraintsRepository<TKey, T>
{
    /// <summary>
    /// 根据Id查询
    /// </summary>
    Task<T> GetByIdAsync(TKey id);
}
#endregion
