using Quartz;

namespace Collapsenav.Net.Tool.Test.Quartz;

public class TestJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("TestJob");
        return Task.FromResult(true);
    }
}