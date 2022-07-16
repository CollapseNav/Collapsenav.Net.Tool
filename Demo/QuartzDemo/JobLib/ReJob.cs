using Quartz;

namespace Collapsenav.Net.Tool.Demo.Quartz;

/// <summary>
/// 更新定时的Job
/// </summary>
public class ReJob : IJob
{
    public ReJob()
    {
    }

    public Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine($"------{DateTime.Now.ToDefaultTimeString()}ReJob");
        return Task.FromResult(true);
    }
}