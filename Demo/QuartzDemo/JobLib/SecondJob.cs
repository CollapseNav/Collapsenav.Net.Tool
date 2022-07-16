using Quartz;

namespace Collapsenav.Net.Tool.Demo.Quartz;

public class SecondJob : IJob
{
    public SecondJob()
    {
    }

    public Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine($"{DateTime.Now.ToDefaultTimeString()}SecondJob");
        return Task.FromResult(true);
    }
}