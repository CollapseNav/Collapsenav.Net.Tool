using System.Linq.Expressions;
using Collapsenav.Net.Tool.Data;
using Microsoft.EntityFrameworkCore;
namespace Collapsenav.Net.Tool.WebApi;

public class QueryRepApplication<T, GetT> : ReadRepApplication<T, GetT>, IQueryApplication<T, GetT>
    where T : class, IEntity
    where GetT : IBaseGet<T>
{
    protected new readonly IQueryRepository<T> Repo;
    protected readonly IMap Mapper;
    public QueryRepApplication(IQueryRepository<T> repository, IMap mapper) : base(repository)
    {
        Repo = repository;
        Mapper = mapper;
    }
    /// <summary>
    /// 带条件分页
    /// </summary>
    public virtual async Task<PageData<T>> QueryPageAsync(GetT input, PageRequest page = null) => await Repo.QueryPageAsync(GetExpression(input), page);
    /// <summary>
    /// 带条件查询(不分页)
    /// </summary>
    public virtual async Task<IEnumerable<T>> QueryAsync(GetT input) => await GetQuery(input).ToListAsync();
    public virtual IQueryable<T> GetQuery(GetT input) => input.GetQuery(Repo.Query(item => true));
    public virtual Expression<Func<T, bool>> GetExpression(GetT input) => input.GetExpression(item => true);
    public virtual async Task<IEnumerable<T>> QueryAsync<NewGetT>(NewGetT input) where NewGetT : class, IBaseGet<T>
    {
        return await input.GetQuery(Repo.Query(item => true)).ToListAsync();
    }

    public virtual async Task<IEnumerable<ReturnT>> QueryAsync<ReturnT>(GetT input)
    {
        var data = await input.GetQuery(Repo.Query(item => true)).ToListAsync();
        return Mapper.Map<IEnumerable<ReturnT>>(data);
    }

    public virtual async Task<IEnumerable<ReturnT>> QueryAsync<NewGetT, ReturnT>(NewGetT input) where NewGetT : class, IBaseGet<T>
    {
        var data = await input.GetQuery(Repo.Query(item => true)).ToListAsync();
        return Mapper.Map<IEnumerable<ReturnT>>(data);
    }
}
public class QueryRepApplication<TKey, T, GetT> : ReadRepApplication<T, GetT>, IQueryApplication<TKey, T, GetT>
    where T : class, IEntity<TKey>
    where GetT : IBaseGet<T>
{
    protected new IQueryRepository<TKey, T> Repo;
    protected IQueryApplication<T, GetT> App;
    public QueryRepApplication(IQueryRepository<TKey, T> repository, IMap mapper) : base(repository)
    {
        Repo = repository;
        App = new QueryRepApplication<T, GetT>(repository, mapper);
    }

    public IQueryable<T> GetQuery(GetT input)
    {
        return App.GetQuery(input);
    }

    /// <summary>
    /// 查找(单个 id)
    /// </summary>
    public virtual async Task<T> QueryAsync(TKey id) => await Repo.QueryAsync(id);

    public virtual async Task<IEnumerable<T>> QueryAsync(GetT input)
    {
        return await App.QueryAsync(input);
    }

    public virtual async Task<IEnumerable<ReturnT>> QueryAsync<ReturnT>(GetT input)
    {
        return await App.QueryAsync<ReturnT>(input);
    }

    public virtual async Task<IEnumerable<T>> QueryAsync<NewGetT>(NewGetT input) where NewGetT : class, IBaseGet<T>
    {
        return await App.QueryAsync(input);
    }

    public virtual async Task<IEnumerable<ReturnT>> QueryAsync<NewGetT, ReturnT>(NewGetT input) where NewGetT : class, IBaseGet<T>
    {
        return await App.QueryAsync<NewGetT, ReturnT>(input);
    }

    /// <summary>
    /// 根据Ids查询
    /// </summary>
    public virtual async Task<IEnumerable<T>> QueryByIdsAsync(IEnumerable<TKey> ids) => await Repo.QueryAsync(ids);

    public virtual async Task<PageData<T>> QueryPageAsync(GetT input, PageRequest page = null)
    {
        return await App.QueryPageAsync(input, page);
    }
}

