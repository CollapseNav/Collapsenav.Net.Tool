using Collapsenav.Net.Tool.Data;
using Microsoft.AspNetCore.Mvc;

namespace Collapsenav.Net.Tool.WebApi;
[ApiController]
[Route("[controller]")]
public class ModifyRepController<T, CreateT> : ControllerBase, IModifyController<T, CreateT>
    where T : class, IEntity
    where CreateT : IBaseCreate<T>
{
    protected readonly IModifyRepository<T> Repository;
    protected readonly IMap Mapper;
    public ModifyRepController(IModifyRepository<T> repository, IMap mapper)
    {
        Repository = repository;
        Mapper = mapper;
    }
    /// <summary>
    /// 添加(单个)
    /// </summary>
    [HttpPost]
    public virtual async Task<T> AddAsync([FromBody] CreateT entity) => await Repository.AddAsync(Mapper.Map<T>(entity));
    /// <summary>
    /// 添加(多个)
    /// </summary>
    [HttpPost, Route("AddRange")]
    public virtual async Task<int> AddRangeAsync(IEnumerable<CreateT> entitys) => await Repository.AddAsync(entitys.Select(item => Mapper.Map<T>(item)));
    /// <summary>
    /// 删除(单个 id)
    /// </summary>
    [HttpDelete, Route("{id}")]
    public virtual async Task DeleteAsync(string id, [FromQuery] bool isTrue = false) => await Repository.DeleteAsync(id, isTrue);
    [NonAction]
    public void Dispose() => Repository.Save();

}
public class ModifyRepController<TKey, T, CreateT> : ModifyRepController<T, CreateT>, IModifyController<TKey, T, CreateT>
    where T : class, IEntity<TKey>
    where CreateT : IBaseCreate<T>
{
    protected new readonly IModifyRepository<TKey, T> Repository;
    protected new readonly IMap Mapper;
    public ModifyRepController(IModifyRepository<TKey, T> repository, IMap mapper) : base(repository, mapper)
    {
        Repository = repository;
        Mapper = mapper;
    }
    [NonAction]
    public override Task DeleteAsync(string id, [FromQuery] bool isTrue = false) => base.DeleteAsync(id, isTrue);
    /// <summary>
    /// 删除(单个 id)
    /// </summary>
    [HttpDelete, Route("{id}")]
    public virtual async Task DeleteAsync(TKey id, [FromQuery] bool isTrue = false) => await Repository.DeleteAsync(id, isTrue);
    /// <summary>
    /// 删除(多个 id)
    /// </summary>
    [HttpDelete]
    public virtual async Task<int> DeleteRangeAsync([FromQuery] IEnumerable<TKey> id, [FromQuery] bool isTrue = false) => await Repository.DeleteAsync(id, isTrue);
    /// <summary>
    /// 更新
    /// </summary>
    [HttpPut, Route("{id}")]
    public virtual async Task UpdateAsync(TKey id, CreateT entity)
    {
        var data = Mapper.Map<T>(entity);
        data.SetValue("Id", id);
        await Repository.UpdateAsync(data);
    }
}

