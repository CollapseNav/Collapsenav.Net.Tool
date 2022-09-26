using Collapsenav.Net.Tool.WebApi;
using DataDemo.EntityLib;

namespace SimpleWebApiDemo.Controllers;

/// <summary>
/// 简单建一个只能查询的 controller
/// </summary>
public class FirstQueryController : QueryAppController<long, FirstEntity, FirstGetDto>
{
    public FirstQueryController(IQueryApplication<long, FirstEntity, FirstGetDto> app) : base(app)
    {
    }
}
