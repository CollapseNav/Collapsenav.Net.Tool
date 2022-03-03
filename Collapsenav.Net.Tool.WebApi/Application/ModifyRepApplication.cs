using Collapsenav.Net.Tool.Data;

namespace Collapsenav.Net.Tool.WebApi;
public class ModifyRepApplication<T, CreateT> : IModifyApplication<T, CreateT>
    where T : class, IEntity
    where CreateT : IBaseCreate<T>
{
    protected readonly IModifyRepository<T> Repository;
    protected readonly IMap Mapper;
    public ModifyRepApplication(IModifyRepository<T> repository, IMap mapper)
    {
        Repository = repository;
        Mapper = mapper;
    }
    /// <summary>
    /// 添加(单个)
    /// </summary>
    public virtual async Task<T> AddAsync(CreateT entity)
    {
        var data = Mapper.Map<T>(entity);
        var result = await Repository.AddAsync(data);
        return result;
    }
    /// <summary>
    /// 添加(多个)
    /// </summary>
    public virtual async Task<int> AddRangeAsync(IEnumerable<CreateT> entitys)
    {
        var result = await Repository.AddAsync(entitys.Select(item => Mapper.Map<T>(item)));
        return result;
    }
    /// <summary>
    /// 删除(单个 id)
    /// </summary>
    public virtual async Task DeleteAsync(string id, bool isTrue = false)
    {
        await Repository.DeleteAsync(id, isTrue);
    }

    public void Dispose()
    {
        Repository.Save();
    }

}
public class ModifyRepApplication<TKey, T, CreateT> : ModifyRepApplication<T, CreateT>, IModifyApplication<TKey, T, CreateT>
    where T : class, IEntity<TKey>
    where CreateT : IBaseCreate<T>
{
    protected new readonly IModifyRepository<TKey, T> Repository;
    protected new readonly IMap Mapper;
    public ModifyRepApplication(IModifyRepository<TKey, T> repository, IMap mapper) : base(repository, mapper)
    {
        Repository = repository;
        Mapper = mapper;
    }

    public override Task DeleteAsync(string id, bool isTrue = false)
    {
        return base.DeleteAsync(id, isTrue);
    }
    /// <summary>
    /// 删除(单个 id)
    /// </summary>
    public virtual async Task DeleteAsync(TKey id, bool isTrue = false)
    {
        await Repository.DeleteAsync(id, isTrue);
    }

    /// <summary>
    /// 删除(多个 id)
    /// </summary>
    public virtual async Task<int> DeleteRangeAsync(IEnumerable<TKey> id, bool isTrue = false)
    {
        var result = await Repository.DeleteAsync(id, isTrue);
        return result;
    }

    /// <summary>
    /// 更新
    /// </summary>
    public virtual async Task UpdateAsync(TKey id, CreateT entity)
    {
        var data = Mapper.Map<T>(entity);
        data.SetValue("Id", id);
        await Repository.UpdateAsync(data);
    }
}

