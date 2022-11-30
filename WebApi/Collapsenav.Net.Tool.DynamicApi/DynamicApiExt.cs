using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection;

namespace Collapsenav.Net.Tool.DynamicApi;

public static class DynamicApiExt
{
    /// <summary>
    /// 判断type是否被标记为 动态api
    /// </summary>
    public static bool IsDynamicApi(this Type type)
    {
        if ((type.HasAttribute<DynamicApiAttribute>() || type.HasInterface(typeof(IDynamicApi))) && !type.IsInterface && !type.IsAbstract)
            return true;
        return false;
    }

    public static IMvcBuilder AddDynamicWebApi(this IMvcBuilder builder)
    {
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
    /// 判断是否有自定义的 route
    /// </summary>
    public static bool HasRouteAttribute(this IList<SelectorModel> selectors)
    {
        return selectors.Any(item => item.AttributeRouteModel != null);
    }

    public static SelectorModel GetRouteSelector(this IList<SelectorModel> selectors)
    {
        return selectors.FirstOrDefault(item => item.AttributeRouteModel != null);
    }

    public static bool HasActionAttribute(this IList<SelectorModel> selectors)
    {
        return selectors.Any(item => item.ActionConstraints.OfType<HttpMethodActionConstraint>().FirstOrDefault()?.HttpMethods?.FirstOrDefault() != null);
    }

    public static SelectorModel GetActionSelector(this IList<SelectorModel> selectors)
    {
        return selectors.FirstOrDefault(item => item.ActionConstraints.OfType<HttpMethodActionConstraint>().FirstOrDefault()?.HttpMethods?.FirstOrDefault() != null);
    }
}