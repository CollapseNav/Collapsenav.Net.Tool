using Collapsenav.Net.Tool.DynamicApi;
using Microsoft.AspNetCore.Authorization;

namespace DynamicWebApi;

public class TestApi : IDynamicApi
{
    public async Task<string> Get()
    {
        return await Task.FromResult("2333");
    }
}