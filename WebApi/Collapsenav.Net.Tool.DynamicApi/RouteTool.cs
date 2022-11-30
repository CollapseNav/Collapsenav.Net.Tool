using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Collapsenav.Net.Tool.DynamicApi;

public class RouteTool
{
    internal static bool HasControllerRoute { get; set; } = false;
    public static string GlobalPrefix { get; set; }

    /// <summary>
    /// 构建 controller route
    /// </summary>
    public static string BuildControllerRoute(ControllerModel controller)
    {
        // 如果有自定义的 Route
        if (controller.Selectors.HasRouteAttribute())
        {
            HasControllerRoute = true;
            // 并且定义了全局前缀
            if (GlobalPrefix.NotEmpty())
            {
                var originSelector = controller.Selectors.First(item => item.AttributeRouteModel != null);
                // 将 全局前缀 和 自定义route 拼接起来
                string route = $@"{GlobalPrefix}/{originSelector.AttributeRouteModel.Template}";
                var selector = new SelectorModel
                {
                    AttributeRouteModel = new AttributeRouteModel(new RouteAttribute(route)),
                };
                // 移除原来的 Selector
                controller.Selectors.Remove(originSelector);
                // 添加修改之后的 Selector
                controller.Selectors.Add(selector);
                return route;
            }
        }
        return "";
    }
    /// <summary>
    /// 构建 action route
    /// </summary>
    public static string BuildRoute(ActionModel action)
    {
        var route = new StringBuilder();
        var controllerName = action.Controller.ControllerName;
        var actionName = action.ActionName;
        // 如果没有 自定义route 则自动添加
        if (!HasControllerRoute)
            route.Append(GetRoute(route, controllerName));

        // 如果没有 自定义route 并且 actionname 不为空
        if (!action.Selectors.HasRouteAttribute() && actionName.NotEmpty())
        {
            var selector = action.Selectors.IsEmpty() ? new SelectorModel() : action.Selectors.FirstOrDefault();
            action.Selectors.Clear();
            action.Selectors.Add(selector);
            if (actionName.EndsWith("Async"))
                actionName = actionName[..^"Async".Length];
            route.Append(GetRoute(route, actionName));

            selector.AttributeRouteModel = new AttributeRouteModel(new RouteAttribute(route.ToString()));
            if (!action.Selectors.HasActionAttribute())
                selector.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { GetHttpMethod(actionName) }));
        }

        if (!action.Selectors.HasActionAttribute())
        {
            var selector = action.Selectors.IsEmpty() ? new SelectorModel() : action.Selectors.FirstOrDefault();
            selector.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { GetHttpMethod(actionName) }));
        }

        return route.ToString();

        static string GetRoute(StringBuilder route, string actionName)
        {
            return $"{(route.Length > 0 ? "/" : "")}{actionName}";
        }
    }

    public static void ConfigureParameters(ActionModel action)
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

    /// <summary>
    /// 根据action名称简单划分类型
    /// </summary>
    private static string GetHttpMethod(string actionName)
    {
        // TODO 此处应该也可以通过配置解决
        if (actionName.HasStartsWith("Get", "Query"))
            return "GET";
        if (actionName.HasStartsWith("Put", "Update"))
            return "PUT";
        if (actionName.HasStartsWith("Delete", "Remove"))
            return "DELETE";
        return "POST";
    }
}