using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Collapsenav.Net.Tool.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Collapsenav.Net.Tool.WebApi
{
    [Route("api/[controller]")]
    public class QueryRepController<TKey, T, GetT> : ControllerBase, IQueryRepController<TKey, T, GetT>
    where T : class, IBaseEntity<TKey>
    where GetT : IBaseGet<T>
    {
        protected readonly IQueryRepository<T> Repository;
        public QueryRepController(IQueryRepository<T> repository) : base()
        {
            Repository = repository;
        }
        /// <summary>
        /// 查找(单个 id)
        /// </summary>
        [HttpGet, Route("{id}")]
        public virtual async Task<T> FindAsync(TKey id) => await Repository.FindAsync(id);
        /// <summary>
        /// 带条件分页
        /// </summary>
        [HttpGet]
        public virtual async Task<PageData<T>> FindPageAsync([FromQuery] GetT input, [FromQuery] PageRequest page = null) => await Repository.FindPageAsync(GetQuery(input), page);
        /// <summary>
        /// 带条件查询(不分页)
        /// </summary>
        [HttpGet, Route("Query")]
        public virtual async Task<IEnumerable<T>> FindQueryAsync([FromQuery] GetT input) => await GetQuery(input).ToListAsync();
        /// <summary>
        /// 根据Ids查询
        /// </summary>
        [HttpGet, Route("ByIds")]
        public virtual async Task<IEnumerable<T>> FindByIdsAsync([FromQuery] IEnumerable<TKey> ids) => await Repository.FindAsync(item => ids.Contains(item.Id));
        /// <summary>
        /// 根据Ids查询
        /// </summary>
        [HttpPost, Route("ByIds")]
        public virtual async Task<IEnumerable<T>> FindByIdsPostAsync(IEnumerable<TKey> ids) => await Repository.FindAsync(item => ids.Contains(item.Id));
        [NonAction]
        public virtual IQueryable<T> GetQuery(GetT input) => input.GetQuery(Repository.Query(item => true));
    }
}

