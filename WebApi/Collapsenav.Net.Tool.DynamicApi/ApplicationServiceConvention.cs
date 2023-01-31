using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Collapsenav.Net.Tool.DynamicApi;
public class ApplicationServiceConvention : IApplicationModelConvention
{
    public void Apply(ApplicationModel application)
    {
        foreach (var controller in application.Controllers)
        {
            if (controller.ControllerType.IsDynamicApi())
                ConfigureApplicationService(controller);
        }
    }

    private static void ConfigureApplicationService(ControllerModel controller)
    {
        // 配置api是否可被 swagger 发现
        if (!controller.ApiExplorer.IsVisible.HasValue)
            controller.ApiExplorer.IsVisible = true;
        foreach (var action in controller.Actions)
        {
            if (!action.ApiExplorer.IsVisible.HasValue)
                action.ApiExplorer.IsVisible = true;
        }
        // 构建 controller  route
        controller.BuildControllerRoute();
        // 构建 action route
        foreach (var action in controller.Actions)
        {
            // 创建 action route
            action.BuildRoute();
            action.ConfigureParameters();
        }
    }
}