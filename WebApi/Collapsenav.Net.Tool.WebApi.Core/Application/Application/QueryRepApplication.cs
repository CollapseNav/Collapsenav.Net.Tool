using System.Linq.Expressions;
using Collapsenav.Net.Tool.Data;
namespace Collapsenav.Net.Tool.WebApi;

public class QueryRepApplication<T, GetT> : ReadRepApplication<T>, IQueryApplication<T, GetT>,
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
    public virtual IQueryable<T> GetQuery(GetT input) => Repo.Query(input.GetExpression());
    public virtual IQueryable<T> GetQuery<NewGetT>(NewGetT input) where NewGetT : IBaseGet<T> => Repo.Query(input.GetExpression());
    public virtual async Task<PageData<T>> QueryPageAsync(GetT input, PageRequest page = null) => await Repo.QueryPageAsync(input.GetExpression(), page);
    public virtual async Task<IEnumerable<T>> QueryAsync(GetT input) => await Repo.QueryDataAsync(GetQuery(input));
    public virtual async Task<IEnumerable<T>> QueryAsync<NewGetT>(NewGetT input) where NewGetT : class, IBaseGet<T> => await Repo.QueryDataAsync(GetQuery(input));
    public virtual async Task<IEnumerable<ReturnT>> QueryAsync<ReturnT>(GetT input) => Mapper.Map<IEnumerable<ReturnT>>(await Repo.QueryDataAsync(GetQuery(input)));
    public virtual async Task<IEnumerable<ReturnT>> QueryAsync<NewGetT, ReturnT>(NewGetT input) where NewGetT : class, IBaseGet<T> => Mapper.Map<IEnumerable<ReturnT>>(await Repo.QueryDataAsync(GetQuery(input)));
    public virtual async Task<bool> IsExistAsync(Expression<Func<T, bool>> exp) => await Repo.IsExistAsync(exp);
    public virtual async Task<int> CountAsync(Expression<Func<T, bool>> exp = null) => await Repo.CountAsync(exp);
}
public class QueryRepApplication<TKey, T, GetT> : QueryRepApplication<T, GetT>, IQueryApplication<TKey, T, GetT>
    where T : class, IEntity<TKey>
    where GetT : IBaseGet<T>
{
    protected new IQueryRepository<TKey, T> Repo;
    public QueryRepApplication(IQueryRepository<TKey, T> repository, IMap mapper) : base(repository, mapper)
    {
        Repo = repository;
    }
    public virtual async Task<T> QueryAsync(TKey id) => await Repo.GetByIdAsync(id);
    public virtual async Task<IEnumerable<T>> QueryByIdsAsync(IEnumerable<TKey> ids) => await Repo.QueryAsync(ids);

}

