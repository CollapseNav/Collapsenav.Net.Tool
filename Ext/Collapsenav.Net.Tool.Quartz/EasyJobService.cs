using Microsoft.Extensions.Hosting;
using Quartz.Spi;

namespace Collapsenav.Net.Tool.Ext;

internal class EasyJobService : IHostedService
{
    private readonly IJobFactory _factory;
    public EasyJobService(IJobFactory factory)
    {
        _factory = factory;
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        if (QuartzNode.Scheduler == null)
            await QuartzNode.InitSchedulerAsync();
        QuartzNode.InitFactory(_factory);
        await QuartzNode.Scheduler.Start(cancellationToken);
        await QuartzNode.Builder?.Build();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(true);
    }
}