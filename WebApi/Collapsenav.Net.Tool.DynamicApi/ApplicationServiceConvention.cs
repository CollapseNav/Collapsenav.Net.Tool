using System.Text;
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

        controller.Selectors.RemoveEmptySelector();
        if (controller.Selectors.Any(temp => temp.AttributeRouteModel != null))
            return;
        foreach (var action in controller.Actions)
        {
            action.Selectors.RemoveEmptySelector();
            if (action.Selectors.IsEmpty())
                AddApplicationServiceSelector(action);
            else
                NormalizeSelectorRoutes(action);
        }

        ConfigureParameters(controller);
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

    private static void NormalizeSelectorRoutes(ActionModel action)
    {
        foreach (var selector in action.Selectors)
        {
            if (selector.AttributeRouteModel == null)
                selector.AttributeRouteModel = new AttributeRouteModel(new RouteAttribute(GenRoute(action)));

            if (selector.ActionConstraints.OfType<HttpMethodActionConstraint>().FirstOrDefault()?.HttpMethods?.FirstOrDefault() == null)
                selector.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { GetHttpMethod(action) }));
        }
    }

    private static void AddApplicationServiceSelector(ActionModel action)
    {
        var selector = new SelectorModel
        {
            AttributeRouteModel = new AttributeRouteModel(new RouteAttribute(GenRoute(action))),
        };
        selector.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { GetHttpMethod(action) }));

        action.Selectors.Add(selector);
    }

    /// <summary>
    /// 生成路径
    /// </summary>
    private static string GenRoute(ActionModel action)
    {
        var routeTemplate = new StringBuilder();
        // TODO 可能需要添加一些自定义前缀
        var controllerName = action.Controller.ControllerName;
        // TODO 可能需要添加去除后缀的功能
        routeTemplate.Append($"{controllerName}");

        var actionName = action.ActionName;
        if (actionName.NotEmpty())
        {
            if (actionName.EndsWith("Async"))
                actionName = actionName[..^"Async".Length];
            routeTemplate.Append($"/{actionName}");
        }

        return routeTemplate.ToString();
    }
    /// <summary>
    /// 根据action名称简单划分类型
    /// </summary>
    private static string GetHttpMethod(ActionModel action)
    {
        var actionName = action.ActionName;
        if (actionName.HasStartsWith("Get", "Query"))
            return "GET";
        if (actionName.HasStartsWith("Put", "Update"))
            return "PUT";
        if (actionName.HasStartsWith("Delete", "Remove"))
            return "DELETE";
        return "POST";
    }
}