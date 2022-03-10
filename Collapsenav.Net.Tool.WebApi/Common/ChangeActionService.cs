using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Collapsenav.Net.Tool.WebApi;

/// <summary>
/// 触发change 应用新增的controller
/// </summary>
public class ChangeActionService : IHostedService
{
    private readonly ApplicationPartManager Part;
    public ChangeActionService(IServiceScopeFactory scope)
    {
        Part = scope.CreateScope().ServiceProvider.GetService<ApplicationPartManager>();
    }
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Part.ApplicationParts.Add(new AssemblyPart(AddNewControllerExt.Ass));
        AddControllerChangeProvider.Instance.HasChanged = true;
        AddControllerChangeProvider.Instance.TokenSource.Cancel();
        await Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}