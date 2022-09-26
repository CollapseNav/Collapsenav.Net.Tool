using Collapsenav.Net.Tool.Data;
using Collapsenav.Net.Tool.WebApi;
using DataDemo.EntityLib;

namespace SimpleWebApiDemo.Controllers;

/// <summary>
/// 添加一个只能新增和删除的 controller
/// </summary>
public class FirstRepModifyController : ModifyRepController<FirstEntity, FirstCreateDto>
{
    public FirstRepModifyController(IModifyRepository<FirstEntity> repository, IMap mapper) : base(repository, mapper)
    {
    }
}
