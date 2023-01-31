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
    public static IServiceCollection AddDynamicWebApi(this IServiceCollection services, ApiJsonConfig config)
    {
        services.AddControllers().AddDynamicWebApi(config.BuildApiConfig());
        return services;
    }
    /// <summary>
    /// 注册动态api
    /// </summary>
    public static IServiceCollection AddDynamicWebApi(this IServiceCollection services, DynamicApiConfig config = null)
    {
        services.AddControllers().AddDynamicWebApi(config ?? new DynamicApiConfig().AddDefault());
        return services;
    }
    /// <summary>
    /// 注册动态api
    /// </summary>
    public static IMvcBuilder AddDynamicWebApi(this IMvcBuilder builder, DynamicApiConfig config = null)
    {
        GlobalConfig = config ?? new();
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
    public static void BuildControllerRoute(this ControllerModel controller)
    {
        GlobalConfig.ConfigControllerRoute(controller);
    }

    /// <summary>
    /// 构建 action route
    /// </summary>
    public static void BuildRoute(this ActionModel action)
    {
        GlobalConfig.ConfigActionRoute(action);
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