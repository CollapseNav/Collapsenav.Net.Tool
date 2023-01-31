using Collapsenav.Net.Tool.DynamicApi;

namespace DynamicWebApi;

[DynamicApi]
public class TestApi
{
    public async Task<string> PostTestApiAsync()
    {
        return await Task.FromResult("2333");
    }
}