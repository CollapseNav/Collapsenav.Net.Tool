using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;

namespace Collapsenav.Net.Tool.DynamicApi;

public static class DynamicApiExt
{
    internal static bool HasControllerRoute { get; set; } = false;
    public static DynamicApiConfig GlobalConfig { get; set; }
    /// <summary>
    /// 判断type是否被标记为 动态api
    /// </summary>
    public static bool IsDynamicApi(this Type type)
    {
        if ((type.HasAttribute<DynamicApiAttribute>() || type.HasInterface(typeof(IDynamicApi))) && !type.IsInterface && !type.IsAbstract)
            return true;
        return false;
    }
    /// <summary>
    /// 注册动态api
    /// </summary>
    public static IServiceCollection AddDynamicWebApi(this IServiceCollection services)
    {
        services.AddControllers().AddDynamicWebApi();
        return services;
    }
    /// <summary>
    /// 注册动态api
    /// </summary>
    public static IMvcBuilder AddDynamicWebApi(this IMvcBuilder builder)
    {
        GlobalConfig = new();
        GlobalConfig.AddDefault();
        // 添加自定义的 DynamicApiProvider 用以识别标记的api
        builder.ConfigureApplicationPartManager(part =>
        {
            part.FeatureProviders.Add(new DynamicApiProvider());
        });
        builder.Services.Configure<MvcOptions>(options =>
        {
            options.Conventions.Add(new ApplicationServiceConvention());
        });
        return builder;
    }
    /// <summary>
    /// 移除空的selector
    /// </summary>
    public static void RemoveEmptySelector(this IList<SelectorModel> selectors)
    {
        // 由于执行过程中需要移除元素, 为了处理方便, 从末尾向前遍历比较好
        for (var i = selectors.Count - 1; i >= 0; i--)
        {
            var item = selectors[i];
            if (item.AttributeRouteModel == null && item.ActionConstraints.IsEmpty() && item.EndpointMetadata.IsEmpty())
                selectors.Remove(item);
        }
    }
    /// <summary>
    /// 移除空的selector
    /// </summary>
    public static void RemoveEmptySelector(this ControllerModel controller)
    {
        controller.Selectors.RemoveEmptySelector();
    }
    /// <summary>
    /// 判断是否有自定义的 route
    /// </summary>
    public static bool HasRouteAttribute(this IList<SelectorModel> selectors)
    {
        return selectors.Any(item => item.AttributeRouteModel != null);
    }
    /// <summary>
    /// 判断是否有自定义的 action 标记
    /// </summary>
    public static bool HasActionAttribute(this IList<SelectorModel> selectors)
    {
        return selectors.Any(item => item.ActionConstraints.OfType<HttpMethodActionConstraint>().FirstOrDefault()?.HttpMethods?.FirstOrDefault() != null);
    }

    /// <summary>
    /// 构建 controller route
    /// </summary>
    /// <remarks>
    /// 如果配置了全局的路由前缀, 则需要将全局前缀拼接到原来的路由之前
    /// </remarks>
    public static string BuildControllerRoute(this ControllerModel controller)
    {
        // 移除空selector
        controller.RemoveEmptySelector();
        // 如果没有定义过 route, 则使用 controllername 定义一个
        if (!controller.Selectors.HasRouteAttribute())
        {
            controller.Selectors.Clear();
            controller.Selectors.Add(new SelectorModel
            {
                AttributeRouteModel = new AttributeRouteModel(new RouteAttribute(controller.ControllerName)),
            });
        }
        // 如果定义了全局前缀, 就需要在原来的route之前加上
        if (GlobalConfig.GlobalPrefix.NotEmpty())
        {
            var newSelectors = controller.Selectors.Select(sel =>
            {
                var route = $@"{GlobalConfig.GlobalPrefix}/{sel.AttributeRouteModel.Template}";
                Console.WriteLine(@$"route= {route}");
                return new SelectorModel { AttributeRouteModel = new AttributeRouteModel(new RouteAttribute(route)), };
            }).ToList();
            controller.Selectors.Clear();
            controller.Selectors.AddRange(newSelectors);
        }
        return "";
    }

    /// <summary>
    /// 构建 action route
    /// </summary>
    public static string BuildRoute(this ActionModel action)
    {
        // 移除空 selector
        action.Selectors.RemoveEmptySelector();
        var route = new StringBuilder();
        var actionName = action.ActionName;

        // 如果没有 自定义route 并且 actionname 不为空
        if (!action.Selectors.HasRouteAttribute() && actionName.NotEmpty())
        {
            var selector = action.Selectors.IsEmpty() ? new SelectorModel() : action.Selectors.FirstOrDefault();
            action.Selectors.Clear();
            action.Selectors.Add(selector);
            if (!action.Selectors.HasActionAttribute())
                selector.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { GlobalConfig.GetHttpMethod(actionName) }));
            actionName = GlobalConfig.Remove(actionName);
            route.Append(GetRoute(route, actionName));

            selector.AttributeRouteModel = new AttributeRouteModel(new RouteAttribute(route.ToString()));
        }

        return route.ToString();

        static string GetRoute(StringBuilder route, string actionName)
        {
            return $"{(route.Length > 0 ? "/" : "")}{actionName}";
        }
    }

    public static void ConfigureParameters(this ActionModel action)
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