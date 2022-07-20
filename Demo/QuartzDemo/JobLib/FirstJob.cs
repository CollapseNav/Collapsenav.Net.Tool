using Quartz;

namespace Collapsenav.Net.Tool.Demo.Quartz;

public class FirstJob : IJob
{
    public FirstJob() { }
    public Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine($"{DateTime.Now.ToDefaultTimeString()}FirstJob");
        return Task.FromResult(true);
    }
}