namespace Collapsenav.Net.Tool.Data;
public interface INoConstraintsReadRepository<T> : INoConstraintsRepository<T>, INoConstraintsCountRepository<T>, INoConstraintsCheckExistRepository<T>
{
    /// <summary>
    /// 根据Id查询
    /// </summary>
    Task<T> GetByIdAsync(object id);
    [Obsolete("统一接口名称, 该方法将被弃用, 使用 QueryAsync 代替")]
    Task<IEnumerable<T>> QueryDataAsync(IQueryable<T> query);
    Task<IEnumerable<T>> QueryAsync(IQueryable<T> query);
    Task<PageData<T>> QueryPageAsync(IQueryable<T> query, PageRequest page = null);
}
public interface INoConstraintsReadRepository<TKey, T> : INoConstraintsReadRepository<T>, INoConstraintsRepository<TKey, T>
{
    /// <summary>
    /// 根据Id查询
    /// </summary>
    Task<T> GetByIdAsync(TKey id);
}
