using System.Threading.Tasks;

namespace AutoInjectWebApiDemo;
public class DefaultService
{
    public async Task<string> GetString()
    {
        return await Task.FromResult("DefaultService");
    }
}