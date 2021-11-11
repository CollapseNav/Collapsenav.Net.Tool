using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Collapsenav.Net.Tool.Data;
using Microsoft.AspNetCore.Mvc;

namespace Collapsenav.Net.Tool.WebApi
{
    public interface IModifyRepController<TKey, T, CreateT, GetT> : IQueryRepController<TKey, T, GetT>, IDisposable
    where T : IBaseEntity<TKey>
    where CreateT : IBaseCreate
    where GetT : IBaseGet<T>
    {
        /// <summary>
        /// 添加(单个)
        /// </summary>
        [HttpPost]
        Task<T> AddAsync([FromBody] CreateT entity);
        /// <summary>
        /// 删除(单个 id)
        /// </summary>
        [HttpDelete, Route("{id}")]
        Task DeleteAsync(TKey id, [FromQuery] bool isTrue = false);
        /// <summary>
        /// 删除(多个 id)
        /// </summary>
        [HttpDelete]
        Task<int> DeleteRangeAsync(IEnumerable<TKey> id, [FromQuery] bool isTrue = false);
        /// <summary>
        /// 更新
        /// </summary>
        [HttpPut, Route("{id}")]
        Task UpdateAsync(TKey id, CreateT entity);
        /// <summary>
        /// 添加(多个)
        /// </summary>
        [HttpPost, Route("AddRange")]
        Task<int> AddRangeAsync(IEnumerable<CreateT> entitys);
    }
}

