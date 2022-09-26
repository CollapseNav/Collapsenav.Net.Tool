using Collapsenav.Net.Tool.Data;
using Collapsenav.Net.Tool.WebApi;
using DataDemo.EntityLib;

namespace SimpleWebApiDemo.Controllers;

/// <summary>
/// 添加一个可以增删改查的 controller
/// </summary>
public class FirstRepCrudController : CrudRepController<FirstEntity, FirstCreateDto, FirstGetDto>
{
    public FirstRepCrudController(ICrudRepository<FirstEntity> repo, IMap mapper) : base(repo, mapper)
    {
    }
}
