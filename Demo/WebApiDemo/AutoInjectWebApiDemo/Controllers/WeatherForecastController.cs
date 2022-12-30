using Collapsenav.Net.Tool.AutoInject;
using Microsoft.AspNetCore.Mvc;

namespace AutoInjectWebApiDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class DefaultController : ControllerBase
{
    [AutoInject]
    public DefaultService Service { get; set; }
    [HttpGet]
    public async Task<string> Get()
    {
        return await Service.GetString();
    }
}
