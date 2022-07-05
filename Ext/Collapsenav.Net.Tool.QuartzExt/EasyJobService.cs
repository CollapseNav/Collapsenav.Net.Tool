using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;

namespace Collapsenav.Net.Tool.Ext;

internal class EasyJobService : IHostedService
{
    private IJobFactory _factory;
    public EasyJobService(IJobFactory factory = null)
    {
        _factory = factory;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        if (QuartzNode.Scheduler == null)
        {
            QuartzNode.Scheduler = await new StdSchedulerFactory().GetScheduler(cancellationToken);
            if (_factory != null)
                QuartzNode.Scheduler.JobFactory = _factory;
            await QuartzNode.Scheduler.Start(cancellationToken);
            await QuartzNode.Builder?.Build(QuartzNode.Scheduler);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }
}