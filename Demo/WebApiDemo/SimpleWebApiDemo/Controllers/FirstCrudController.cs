using Collapsenav.Net.Tool.WebApi;
using DataDemo.EntityLib;

namespace SimpleWebApiDemo.Controllers;

/// <summary>
/// 添加一个可以增删改查的 controller
/// </summary>
public class FirstCrudController : CrudAppController<FirstEntity, FirstCreateDto, FirstGetDto>
{
    public FirstCrudController(ICrudApplication<FirstEntity, FirstCreateDto, FirstGetDto> app, IMap mapper) : base(app, mapper)
    {
    }
}
