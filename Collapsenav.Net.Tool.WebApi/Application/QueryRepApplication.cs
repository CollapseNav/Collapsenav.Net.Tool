using System.Linq.Expressions;
using Collapsenav.Net.Tool.Data;
using Microsoft.EntityFrameworkCore;
namespace Collapsenav.Net.Tool.WebApi;

public class QueryRepApplication<T, GetT> : IQueryApplication<T, GetT>
    where T : class, IEntity
    where GetT : IBaseGet<T>
{
    protected readonly IQueryRepository<T> Repository;
    public QueryRepApplication(IQueryRepository<T> repository) : base()
    {
        Repository = repository;
    }
    /// <summary>
    /// 带条件分页
    /// </summary>
    public virtual async Task<PageData<T>> QueryPageAsync(GetT input, PageRequest page = null) => await Repository.QueryPageAsync(GetExpression(input), page);
    /// <summary>
    /// 带条件查询(不分页)
    /// </summary>
    public virtual async Task<IEnumerable<T>> QueryAsync(GetT input) => await GetQuery(input).ToListAsync();
    public virtual IQueryable<T> GetQuery(GetT input) => input.GetQuery(Repository.Query(item => true));
    public virtual Expression<Func<T, bool>> GetExpression(GetT input) => input.GetExpression(item => true);
    /// <summary>
    /// 查找(单个 id)
    /// </summary>
    public virtual async Task<T> QueryAsync(string id) => await Repository.QueryAsync(id);
}
public class QueryRepApplication<TKey, T, GetT> : QueryRepApplication<T, GetT>, IQueryApplication<TKey, T, GetT>
    where T : class, IEntity<TKey>
    where GetT : IBaseGet<T>
{
    protected new IQueryRepository<TKey, T> Repository;
    public QueryRepApplication(IQueryRepository<TKey, T> repository) : base(repository)
    {
        Repository = repository;
    }

    public override Task<T> QueryAsync(string id)
    {
        return base.QueryAsync(id);
    }

    /// <summary>
    /// 查找(单个 id)
    /// </summary>
    public virtual async Task<T> QueryAsync(TKey id) => await Repository.QueryAsync(id);
    /// <summary>
    /// 根据Ids查询
    /// </summary>
    public virtual async Task<IEnumerable<T>> QueryByIdsAsync(IEnumerable<TKey> ids) => await Repository.QueryAsync(ids);

}

