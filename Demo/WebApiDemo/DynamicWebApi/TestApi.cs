using Collapsenav.Net.Tool.DynamicApi;

namespace DynamicWebApi;

[DynamicApi]
public class TestApi
{
    public async Task<string> EditTestApiAsync()
    {
        return await Task.FromResult("2333");
    }
}