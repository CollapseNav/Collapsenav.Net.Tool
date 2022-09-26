using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;

public class CrudRepApplication<T, CreateT, GetT> : ICrudApplication<T, CreateT, GetT>
    where T : class, IEntity
    where CreateT : IBaseCreate<T>
    where GetT : IBaseGet<T>
{
    protected ICrudRepository<T> Repo;
    protected IModifyApplication<T, CreateT> Write;
    protected IQueryApplication<T, GetT> Read;
    public CrudRepApplication(ICrudRepository<T> repo, IMap mapper)
    {
        Repo = repo;
        Write = new ModifyRepApplication<T, CreateT>(Repo, mapper);
        Read = new QueryRepApplication<T, GetT>(Repo, mapper);
    }
    public virtual void Save() => Repo.Save();
    public virtual async Task SaveAsync() => await Repo.SaveAsync();
    public virtual async Task<T> AddAsync(CreateT entity) => await Write.AddAsync(entity);
    public virtual async Task<int> AddRangeAsync(IEnumerable<CreateT> entitys) => await Write.AddRangeAsync(entitys);
    public virtual async Task<PageData<T>> QueryPageAsync(GetT input, PageRequest page = null) => await Read.QueryPageAsync(input, page);
    public virtual async Task<IEnumerable<T>> QueryAsync(GetT input) => await Read.QueryAsync(input);
    public virtual void Dispose() => Repo.Save();
    public virtual IQueryable<T> GetQuery(GetT input) => Read.GetQuery(input);
    public virtual async Task<T> QueryByStringIdAsync(string id) => await Read.QueryByStringIdAsync(id);
    public virtual async Task<bool> DeleteAsync(string id, bool isTrue = false) => await Write.DeleteAsync(id, isTrue);
    public virtual async Task<IEnumerable<T>> QueryAsync<NewGetT>(NewGetT input) where NewGetT : class, IBaseGet<T> => await Read.QueryAsync(input);
    public virtual async Task<IEnumerable<ReturnT>> QueryAsync<ReturnT>(GetT input) => await Read.QueryAsync<ReturnT>(input);
    public virtual async Task<IEnumerable<ReturnT>> QueryAsync<NewGetT, ReturnT>(NewGetT input) where NewGetT : class, IBaseGet<T> => await Read.QueryAsync<NewGetT, ReturnT>(input);
    public virtual async Task<T> AddAsync(T entity) => await Write.AddAsync(entity);
    public virtual async Task<int> UpdateAsync(string id, T entity) => await Write.UpdateAsync(id, entity);
    public virtual IQueryable<T> GetQuery<NewGetT>(NewGetT input) where NewGetT : IBaseGet<T> => Read.GetQuery(input);
}
public class CrudRepApplication<TKey, T, CreateT, GetT> : CrudRepApplication<T, CreateT, GetT>, ICrudApplication<TKey, T, CreateT, GetT>
    where T : class, IEntity<TKey>
    where CreateT : IBaseCreate<T>
    where GetT : IBaseGet<T>
{
    protected new ICrudRepository<TKey, T> Repo;
    protected new IModifyApplication<TKey, T, CreateT> Write;
    protected new IQueryApplication<TKey, T, GetT> Read;
    public CrudRepApplication(ICrudRepository<TKey, T> repo, IMap mapper) : base(repo, mapper)
    {
        Repo = repo;
        Write = new ModifyRepApplication<TKey, T, CreateT>(Repo, mapper);
        Read = new QueryRepApplication<TKey, T, GetT>(Repo, mapper);
    }
    public virtual async Task<bool> DeleteAsync(TKey id, bool isTrue = false) => await Write.DeleteAsync(id, isTrue);
    public override Task<bool> DeleteAsync(string id, bool isTrue = false) => base.DeleteAsync(id, isTrue);
    public virtual async Task<int> DeleteRangeAsync(IEnumerable<TKey> id, bool isTrue = false) => await Write.DeleteRangeAsync(id, isTrue);
    public virtual async Task<int> UpdateAsync(TKey id, CreateT entity) => await Write.UpdateAsync(id, entity);
    public virtual async Task<T> QueryAsync(TKey id) => await Read.QueryAsync(id);
    public override Task<T> QueryByStringIdAsync(string id) => base.QueryByStringIdAsync(id);
    public virtual async Task<IEnumerable<T>> QueryByIdsAsync(IEnumerable<TKey> ids) => await Read.QueryByIdsAsync(ids);
}