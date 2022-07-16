using Quartz;

namespace Collapsenav.Net.Tool.Demo.Quartz;

/// <summary>
/// 通过QuartzBuilder添加的Job
/// </summary>
public class UseBuilderJob : IJob
{
    public UseBuilderJob()
    {
    }

    public Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine($"++++++{DateTime.Now.ToDefaultTimeString()}UseBuilderJob");
        return Task.FromResult(true);
    }
}