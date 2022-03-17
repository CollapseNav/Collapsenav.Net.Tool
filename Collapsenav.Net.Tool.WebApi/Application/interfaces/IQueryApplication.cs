using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;
public interface IQueryApplication<T, GetT> : IReadApplication<T>
    where T : IEntity
    where GetT : IBaseGet<T>
{
    /// <summary>
    /// 获取query
    /// </summary>
    IQueryable<T> GetQuery(GetT input);
    /// <summary>
    /// 分页查询
    /// </summary>
    Task<PageData<T>> QueryPageAsync(GetT input, PageRequest page = null);
    /// <summary>
    /// 列表查询
    /// </summary>
    Task<IEnumerable<T>> QueryAsync(GetT input);
    Task<IEnumerable<ReturnT>> QueryAsync<ReturnT>(GetT input);
    Task<IEnumerable<T>> QueryAsync<NewGetT>(NewGetT input) where NewGetT : class, IBaseGet<T>;
    Task<IEnumerable<ReturnT>> QueryAsync<NewGetT, ReturnT>(NewGetT input) where NewGetT : class, IBaseGet<T>;
}
public interface IQueryApplication<TKey, T, GetT> : IQueryApplication<T, GetT>,
IReadApplication<TKey, T>
    where T : IEntity<TKey>
    where GetT : IBaseGet<T>
{
    /// <summary>
    /// 根据Id查询
    /// </summary>
    Task<IEnumerable<T>> QueryByIdsAsync(IEnumerable<TKey> ids);
}
