using Collapsenav.Net.Tool.DynamicApi;
using Microsoft.AspNetCore.Authorization;

namespace DynamicWebApi;

[Authorize]
public class TestApi : IDynamicApi
{
    public async Task<string> String()
    {
        return await Task.FromResult("2333");
    }
}