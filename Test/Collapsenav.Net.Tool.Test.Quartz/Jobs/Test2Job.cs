using Quartz;

namespace Collapsenav.Net.Tool.Test.Quartz;

public class Test2Job : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("Test2Job");
        return Task.FromResult(true);
    }
}