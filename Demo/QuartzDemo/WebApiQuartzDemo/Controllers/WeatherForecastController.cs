using Collapsenav.Net.Tool.Demo.Quartz;
using Collapsenav.Net.Tool.Ext;
using Microsoft.AspNetCore.Mvc;
using Quartz;

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

    /// <summary>
    /// 重设 ReJob 的定时
    /// </summary>
    [HttpGet("ByLen")]
    public async Task ReJobByLen([FromQuery] int second = 1)
    {
        await QuartzNode.Scheduler.RescheduleJob<ReJob>(second, new TriggerKey("ReJob123", "ReJob123"));
    }
    /// <summary>
    /// 重设 ReJob 的定时
    /// </summary>
    [HttpGet("ByCron")]
    public async Task ReJobByCron([FromQuery] string cron = "0/1 * * * * ?")
    {
        await QuartzNode.Scheduler.RescheduleJob<ReJob>(cron, new TriggerKey("ReJob123", "ReJob123"));
    }

    [HttpGet("TestQuartzStatus")]
    public async Task<IReadOnlyCollection<JobStatus>> TestQuartzStatus()
    {
        var status = await QuartzNode.Scheduler.GetJobStatus();
        return status;
    }
}
