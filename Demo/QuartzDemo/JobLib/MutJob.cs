using Quartz;

namespace Collapsenav.Net.Tool.Demo.Quartz;

public class MutJob : IJob
{
    public MutJob()
    {
    }

    public Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine($"{DateTime.Now.ToDefaultTimeString()}MutJob");
        return Task.FromResult(true);
    }
}