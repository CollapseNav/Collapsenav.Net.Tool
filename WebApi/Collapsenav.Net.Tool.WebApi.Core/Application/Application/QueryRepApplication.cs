using System.Linq.Expressions;
using Collapsenav.Net.Tool.Data;
namespace Collapsenav.Net.Tool.WebApi;

public class QueryRepApplication<T, GetT> : ReadRepApplication<T, GetT>, IQueryApplication<T, GetT>,
ICountApplication<T>, ICheckExistApplication<T>
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
    public virtual async Task<PageData<T>> QueryPageAsync(GetT input, PageRequest page = null) => await Repo.QueryPageAsync(GetExpression(input), page);
    public virtual async Task<IEnumerable<T>> QueryAsync(GetT input) => await Repo.QueryDataAsync(GetQuery(input));
    public virtual IQueryable<T> GetQuery(GetT input) => input.GetQuery(Repo.Query(item => true));
    public virtual Expression<Func<T, bool>> GetExpression(GetT input) => input.GetExpression(item => true);
    public virtual async Task<IEnumerable<T>> QueryAsync<NewGetT>(NewGetT input) where NewGetT : class, IBaseGet<T>
    {
        return await Repo.QueryDataAsync(input.GetQuery(Repo.Query(item => true)));
    }

    public virtual async Task<IEnumerable<ReturnT>> QueryAsync<ReturnT>(GetT input)
    {
        var data = await Repo.QueryDataAsync(input.GetQuery(Repo.Query(item => true)));
        return Mapper.Map<IEnumerable<ReturnT>>(data);
    }

    public virtual async Task<IEnumerable<ReturnT>> QueryAsync<NewGetT, ReturnT>(NewGetT input) where NewGetT : class, IBaseGet<T>
    {
        var data = await Repo.QueryDataAsync(input.GetQuery(Repo.Query(item => true)));
        return Mapper.Map<IEnumerable<ReturnT>>(data);
    }
    public virtual async Task<bool> IsExistAsync(Expression<Func<T, bool>> exp)
    {
        return await Repo.IsExistAsync(exp);
    }
    public virtual async Task<int> CountAsync(Expression<Func<T, bool>> exp = null)
    {
        return await Repo.CountAsync(exp);
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

    public virtual async Task<T> QueryAsync(TKey id) => await Repo.GetByIdAsync(id);

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

    public virtual async Task<IEnumerable<T>> QueryByIdsAsync(IEnumerable<TKey> ids) => await Repo.QueryAsync(ids);

    public virtual async Task<PageData<T>> QueryPageAsync(GetT input, PageRequest page = null)
    {
        return await App.QueryPageAsync(input, page);
    }
}

