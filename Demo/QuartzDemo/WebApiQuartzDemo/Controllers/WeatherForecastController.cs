using Collapsenav.Net.Tool.Ext;
using Microsoft.AspNetCore.Mvc;

namespace WebApiQuartzDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }
    [HttpGet("TestQuartzStatus")]
    public async Task<IReadOnlyCollection<JobStatus>> TestQuartzStatus()
    {
        var status = await QuartzNode.Scheduler.GetJobStatus();
        return status;
    }
}
