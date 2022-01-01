using AutoMapper;
using Collapsenav.Net.Tool.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Collapsenav.Net.Tool.WebApi;
[Route("api/[controller]")]
public class ModifyRepController<TKey, T, CreateT, GetT> : QueryRepController<TKey, T, GetT>, IModifyRepController<TKey, T, CreateT, GetT>
    where T : class, IBaseEntity<TKey>
    where CreateT : IBaseCreate<T>
    where GetT : IBaseGet<T>
{
    protected new readonly IModifyRepository<T> Repository;
    protected readonly IMapper Mapper;
    public ModifyRepController(IModifyRepository<T> repository, IMapper mapper) : base(repository)
    {
        Repository = repository;
        Mapper = mapper;
    }

    /// <summary>
    /// 添加(单个)
    /// </summary>
    [HttpPost]
    public virtual async Task<T> AddAsync([FromBody] CreateT entity)
    {
        var data = Mapper.Map<T>(entity);
        var result = await Repository.AddAsync(data);
        return result;
    }

    /// <summary>
    /// 删除(单个 id)
    /// </summary>
    [HttpDelete, Route("{id}")]
    public virtual async Task DeleteAsync(TKey id, [FromQuery] bool isTrue = false)
    {
        await Repository.DeleteAsync(id, isTrue);
    }

    /// <summary>
    /// 删除(多个 id)
    /// </summary>
    [HttpDelete]
    public virtual async Task<int> DeleteRangeAsync([FromQuery] IEnumerable<TKey> id, [FromQuery] bool isTrue = false)
    {
        var result = await Repository.DeleteAsync(item => id.Contains(item.Id), isTrue);
        return result;
    }

    /// <summary>
    /// 更新
    /// </summary>
    [HttpPut, Route("{id}")]
    public virtual async Task UpdateAsync(TKey id, CreateT entity)
    {
        var data = Mapper.Map<T>(entity);
        data.Id = id;
        await Repository.UpdateAsync(data);
    }

    /// <summary>
    /// 添加(多个)
    /// </summary>
    [HttpPost, Route("AddRange")]
    public virtual async Task<int> AddRangeAsync(IEnumerable<CreateT> entitys)
    {
        var result = await Repository.AddAsync(entitys.Select(item => Mapper.Map<T>(item)));
        return result;
    }

    public void Dispose()
    {
        Repository.Save();
    }
}

