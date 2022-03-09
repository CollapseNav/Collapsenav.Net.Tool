using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;
public interface IQueryApplication<T, GetT> : IApplication
    where T : IEntity
    where GetT : IBaseGet<T>
{
    IQueryable<T> GetQuery(GetT input);
    /// <summary>
    /// 带条件分页
    /// </summary>
    Task<PageData<T>> QueryPageAsync(GetT input, PageRequest page = null);
    /// <summary>
    /// 带条件查询(不分页)
    /// </summary>
    Task<IEnumerable<T>> QueryAsync(GetT input);
    /// <summary>
    /// 查找(单个 id)
    /// </summary>
    Task<T> QueryAsync(string id);
    Task<IEnumerable<T>> QueryAsync<NewGetT>(NewGetT input) where NewGetT : IBaseGet<T>;
    Task<IEnumerable<ReturnT>> QueryAsync<ReturnT>(GetT input);
    Task<IEnumerable<ReturnT>> QueryAsync<NewGetT, ReturnT>(NewGetT input) where NewGetT : IBaseGet<T>;
}
public interface IQueryApplication<TKey, T, GetT> : IQueryApplication<T, GetT>
    where T : IEntity<TKey>
    where GetT : IBaseGet<T>
{
    /// <summary>
    /// 查找(单个 id)
    /// </summary>
    Task<T> QueryAsync(TKey id);
    /// <summary>
    /// 根据Id查询
    /// </summary>
    Task<IEnumerable<T>> QueryByIdsAsync(IEnumerable<TKey> ids);
}
