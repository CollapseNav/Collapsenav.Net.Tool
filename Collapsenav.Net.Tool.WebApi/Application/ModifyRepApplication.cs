using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;
public class ModifyRepApplication<T, CreateT> : WriteRepApplication<T>, IModifyApplication<T, CreateT>
    where T : class, IEntity
    where CreateT : IBaseCreate<T>
{
    protected new readonly IModifyRepository<T> Repo;
    protected readonly IMap Mapper;
    public ModifyRepApplication(IModifyRepository<T> repository, IMap mapper) : base(repository)
    {
        Repo = repository;
        Mapper = mapper;
    }
    /// <summary>
    /// 添加(单个)
    /// </summary>
    public virtual async Task<T> AddAsync(CreateT entity)
    {
        var data = Mapper.Map<T>(entity);
        var result = await Repo.AddAsync(data);
        return result;
    }
    /// <summary>
    /// 添加(多个)
    /// </summary>
    public virtual async Task<int> AddRangeAsync(IEnumerable<CreateT> entitys)
    {
        var result = await Repo.AddAsync(entitys.Select(item => Mapper.Map<T>(item)));
        return result;
    }
}
public class ModifyRepApplication<TKey, T, CreateT> : WriteRepApplication<TKey, T>, IModifyApplication<TKey, T, CreateT>
    where T : class, IEntity<TKey>
    where CreateT : IBaseCreate<T>
{
    protected new readonly IModifyRepository<TKey, T> Repo;
    protected IModifyApplication<T, CreateT> App;
    protected readonly IMap Mapper;
    public ModifyRepApplication(IModifyRepository<TKey, T> repository, IMap mapper) : base(repository)
    {
        Repo = repository;
        Mapper = mapper;
        App = new ModifyRepApplication<T, CreateT>(repository, mapper);
    }

    public virtual async Task<T> AddAsync(CreateT entity)
    {
        return await App.AddAsync(entity);
    }

    public virtual async Task<int> AddRangeAsync(IEnumerable<CreateT> entitys)
    {
        return await App.AddRangeAsync(entitys);
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
        var result = await Repo.DeleteAsync(id, isTrue);
        return result;
    }
    /// <summary>
    /// 更新
    /// </summary>
    public virtual async Task UpdateAsync(TKey id, CreateT entity)
    {
        var data = Mapper.Map<T>(entity);
        data.SetValue("Id", id);
        await Repo.UpdateAsync(data);
    }
}

