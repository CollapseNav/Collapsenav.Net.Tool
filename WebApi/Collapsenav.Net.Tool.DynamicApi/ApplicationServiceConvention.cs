using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
        // 移除空selector
        controller.Selectors.RemoveEmptySelector();
        // 构建 controller  route
        RouteTool.BuildControllerRoute(controller);
        // 构建 action route
        foreach (var action in controller.Actions)
        {
            // 移除空 selector
            action.Selectors.RemoveEmptySelector();
            // 创建 action route
            RouteTool.BuildRoute(action);
            RouteTool.ConfigureParameters(action);
        }

        // ConfigureParameters(controller);
    }

    private static void ConfigureParameters(ControllerModel controller)
    {
        foreach (var action in controller.Actions)
        {
            foreach (var parameter in action.Parameters)
            {
                if (parameter.BindingInfo != null)
                    continue;

                if (parameter.ParameterType.IsClass &&
                    parameter.ParameterType != typeof(string) &&
                    parameter.ParameterType != typeof(IFormFile))
                {
                    var httpMethods = action.Selectors.SelectMany(temp => temp.ActionConstraints).OfType<HttpMethodActionConstraint>().SelectMany(temp => temp.HttpMethods).ToList();
                    if (httpMethods.HasContain("GET", "DELETE", "TRACE", "HEAD"))
                        continue;
                    parameter.BindingInfo = BindingInfo.GetBindingInfo(new[] { new FromBodyAttribute() });
                }
            }
        }
    }
}