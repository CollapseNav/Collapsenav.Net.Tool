using Collapsenav.Net.Tool.Ext;
using Microsoft.AspNetCore.Mvc;

namespace RedisDemo.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IRedisCache cache;
    public WeatherForecastController(IRedisCache cache)
    {
        this.cache = cache;
    }

    [HttpGet("GetCache")]
    public IEnumerable<string> Get([FromQuery] string key)
    {
        return new[] { cache.GetCache(key) };
    }
    [HttpGet("HGetCache")]
    public IEnumerable<string> HGet([FromQuery] string key, [FromQuery] string field)
    {
        return new[] { cache.GetHash(key, field) };
    }

    [HttpGet("AddCache")]
    public void Add([FromQuery] string key)
    {
        cache.SetCache(key, key + key);
    }
    [HttpGet("HAddCache")]
    public void HAdd([FromQuery] string key, [FromQuery] string filed)
    {
        cache.SetHash(key, key, key + filed);
    }

    [HttpGet("Refresh")]
    public void Refresh([FromQuery] string key)
    {
        cache.RefreshCache(key);
    }

    [HttpGet("GetKeys")]
    public IEnumerable<string> GetKeys([FromQuery] string value)
    {
        return cache.GetKeys(value);
    }
}
