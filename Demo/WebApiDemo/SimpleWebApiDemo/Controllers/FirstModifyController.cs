using Collapsenav.Net.Tool.WebApi;
using DataDemo.EntityLib;

namespace SimpleWebApiDemo.Controllers;

/// <summary>
/// 添加一个只能新增和删除的 controller
/// </summary>
public class FirstModifyController : ModifyAppController<FirstEntity, FirstCreateDto>
{
    public FirstModifyController(IModifyApplication<FirstEntity, FirstCreateDto> app, IMap mapper) : base(app, mapper)
    {
    }
}
