using Quartz;

namespace Collapsenav.Net.Tool.Demo.Quartz;

/// <summary>
/// 使用依赖注入的Job
/// </summary>
public class DIJob : IJob
{
    private readonly DIModel _model;
    public DIJob()
    {
    }
    // public DIJob(DIModel model)
    // {
    //     Console.WriteLine($"Inject Model: {model.Name}");
    //     _model = model;
    // }

    public Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine(_model.Name);
        return Task.FromResult(true);
    }
}