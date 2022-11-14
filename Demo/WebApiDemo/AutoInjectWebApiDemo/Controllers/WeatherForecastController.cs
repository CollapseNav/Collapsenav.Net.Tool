using Collapsenav.Net.Tool.AutoInject;
using Microsoft.AspNetCore.Mvc;

namespace AutoInjectWebApiDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    [AutoInject]
    public DefaultService Service { get; set; }
    [HttpGet]
    public string Get()
    {
        return Service.GetString();
    }
}
