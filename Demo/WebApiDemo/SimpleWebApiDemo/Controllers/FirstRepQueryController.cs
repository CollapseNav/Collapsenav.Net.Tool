using Collapsenav.Net.Tool.Data;
using Collapsenav.Net.Tool.WebApi;
using DataDemo.EntityLib;

namespace SimpleWebApiDemo.Controllers;

/// <summary>
/// 简单建一个只能查询的 controller
/// </summary>
public class FirstRepQueryController : QueryRepController<FirstEntity, FirstGetDto>
{
    public FirstRepQueryController(IQueryRepository<FirstEntity> repository) : base(repository)
    {
    }
}
