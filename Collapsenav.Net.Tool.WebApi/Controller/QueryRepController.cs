using System.Linq.Expressions;
using Collapsenav.Net.Tool.Data;
using Collapsenav.Net.Tool.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Collapsenav.Net.Tool.WebApi;
[ApiController]
[Route("[controller]")]
public class QueryRepController<TKey, T, GetT> : ControllerBase, IQueryRepController<TKey, T, GetT>, IExcelExportController<T, GetT>
    where T : class, IBaseEntity<TKey>
    where GetT : IBaseGet<T>
{
    protected readonly IQueryRepository<TKey, T> Repository;
    public QueryRepController(IQueryRepository<TKey, T> repository) : base()
    {
        Repository = repository;
    }
    /// <summary>
    /// 查找(单个 id)
    /// </summary>
    [HttpGet, Route("{id}")]
    public virtual async Task<T> QueryAsync(TKey id) => await Repository.QueryAsync(id);
    /// <summary>
    /// 带条件分页
    /// </summary>
    [HttpGet]
    public virtual async Task<PageData<T>> QueryPageAsync([FromQuery] GetT input, [FromQuery] PageRequest page = null) => await Repository.QueryPageAsync(GetExpression(input), page);
    /// <summary>
    /// 带条件查询(不分页)
    /// </summary>
    [HttpGet, Route("Query")]
    public virtual async Task<IEnumerable<T>> QueryAsync([FromQuery] GetT input) => await GetQuery(input).ToListAsync();
    /// <summary>
    /// 根据Ids查询
    /// </summary>
    [HttpGet, Route("ByIds")]
    public virtual async Task<IEnumerable<T>> QueryByIdsAsync([FromQuery] IEnumerable<TKey> ids) => await Repository.QueryAsync(ids);
    /// <summary>
    /// 根据Ids查询
    /// </summary>
    [HttpPost, Route("ByIds")]
    public virtual async Task<IEnumerable<T>> QueryByIdsPostAsync(IEnumerable<TKey> ids) => await Repository.QueryAsync(ids);
    [NonAction]
    public virtual IQueryable<T> GetQuery(GetT input) => input.GetQuery(Repository.Query(item => true));
    [NonAction]
    public virtual Expression<Func<T, bool>> GetExpression(GetT input) => input.GetExpression(item => true);

    /// <summary>
    /// 导出为Excel
    /// </summary>
    [HttpGet("Export")]
    public async Task<FileStreamResult> ExportExcelAsync([FromQuery] GetT input)
    {
        var data = await QueryAsync(input);
        var defaultConfig = ExportConfig<T>.GenDefaultConfig(data);
        return File(await defaultConfig.EPPlusExportAsync(new MemoryStream()), "application/octet-stream", $@"{DateTime.Now:yyyyMMddHHmmssfff}.xlsx");
    }
}

