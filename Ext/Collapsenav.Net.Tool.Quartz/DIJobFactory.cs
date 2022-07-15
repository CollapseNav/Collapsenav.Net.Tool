using Quartz;
using Quartz.Spi;

namespace Collapsenav.Net.Tool.Ext;

public class DIJobFactory : IJobFactory
{
    private readonly IServiceProvider _serviceProvider;

    public DIJobFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        // 直接尝试创建job，针对无参构造函数的job
        var type = bundle.JobDetail.JobType;
        try
        {
            if (Activator.CreateInstance(type) is IJob job)
                return job;
        }
        catch { }
        // 无法无参构造时通过DI创建job
        return _serviceProvider.GetService(type) as IJob;
    }

    public void ReturnJob(IJob job)
    {
        var disposable = job as IDisposable;
        disposable?.Dispose();
    }
}
