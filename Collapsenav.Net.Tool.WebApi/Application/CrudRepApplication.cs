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
    /// <summary>
    /// 添加(单个)
    /// </summary>
    public virtual async Task<T> AddAsync(CreateT entity)
    {
        return await Write.AddAsync(entity);
    }

    /// <summary>
    /// 添加(多个)
    /// </summary>
    public virtual async Task<int> AddRangeAsync(IEnumerable<CreateT> entitys)
    {
        return await Write.AddRangeAsync(entitys);
    }
    /// <summary>
    /// 带条件分页
    /// </summary>
    public virtual async Task<PageData<T>> QueryPageAsync(GetT input, PageRequest page = null)
    {
        return await Read.QueryPageAsync(input, page);
    }
    /// <summary>
    /// 带条件查询(不分页)
    /// </summary>
    public virtual async Task<IEnumerable<T>> QueryAsync(GetT input)
    {
        return await Read.QueryAsync(input);
    }
    public virtual void Dispose()
    {
        Repo.Save();
    }
    public virtual IQueryable<T> GetQuery(GetT input)
    {
        return Read.GetQuery(input);
    }
    /// <summary>
    /// 查找(单个 id)
    /// </summary>
    public virtual async Task<T> QueryByStringIdAsync(string id)
    {
        return await Read.QueryByStringIdAsync(id);
    }
    /// <summary>
    /// 查找(单个 id)
    /// </summary>
    public virtual async Task DeleteAsync(string id, bool isTrue = false)
    {
        await Write.DeleteAsync(id, isTrue);
    }

    public virtual async Task<IEnumerable<T>> QueryAsync<NewGetT>(NewGetT input) where NewGetT : class, IBaseGet<T>
    {
        return await Read.QueryAsync(input);
    }

    public virtual async Task<IEnumerable<ReturnT>> QueryAsync<ReturnT>(GetT input)
    {
        return await Read.QueryAsync<ReturnT>(input);
    }

    public virtual async Task<IEnumerable<ReturnT>> QueryAsync<NewGetT, ReturnT>(NewGetT input) where NewGetT : class, IBaseGet<T>
    {
        return await Read.QueryAsync<NewGetT, ReturnT>(input);
    }
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

    /// <summary>
    /// 删除(单个 id)
    /// </summary>
    public virtual async Task DeleteAsync(TKey id, bool isTrue = false)
    {
        await Write.DeleteAsync(id, isTrue);
    }

    public override Task DeleteAsync(string id, bool isTrue = false)
    {
        return base.DeleteAsync(id, isTrue);
    }

    /// <summary>
    /// 删除(多个 id)
    /// </summary>
    public virtual async Task<int> DeleteRangeAsync(IEnumerable<TKey> id, bool isTrue = false)
    {
        return await Write.DeleteRangeAsync(id, isTrue);
    }
    /// <summary>
    /// 更新
    /// </summary>
    public virtual async Task UpdateAsync(TKey id, CreateT entity)
    {
        await Write.UpdateAsync(id, entity);
    }

    /// <summary>
    /// 查找(单个 id)
    /// </summary>
    public virtual async Task<T> QueryAsync(TKey id)
    {
        return await Read.QueryAsync(id);
    }
    public override Task<T> QueryByStringIdAsync(string id)
    {
        return base.QueryByStringIdAsync(id);
    }
    /// <summary>
    /// 根据Ids查询
    /// </summary>
    public virtual async Task<IEnumerable<T>> QueryByIdsAsync(IEnumerable<TKey> ids)
    {
        return await Read.QueryByIdsAsync(ids);
    }
}