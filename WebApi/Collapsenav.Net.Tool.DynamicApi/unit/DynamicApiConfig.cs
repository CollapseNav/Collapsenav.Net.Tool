using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace Collapsenav.Net.Tool.DynamicApi;

public class DynamicApiConfig
{
    public string GlobalPrefix { get; set; }
    public List<string> GetPrefix { get; set; } = new();
    public List<string> PostPrefix { get; set; } = new();
    public List<string> PutPrefix { get; set; } = new();
    public List<string> DeletePrefix { get; set; } = new();
    public List<string> PrefixList { get; set; } = new();
    public List<string> SuffixList { get; set; } = new();

    private readonly string[] DefaultPrefix = new[] { "Get", "Query", "Post", };
    private readonly string[] DefaultSuffix = new[] { "Async" };
    private readonly string[] DefaultGetPrefix = new[] { "Get", "Query" };
    private readonly string[] DefaultPostPrefix = new[] { "Add", "Create", "Post" };
    private readonly string[] DefaultPutPrefix = new[] { "Put", "Update" };
    private readonly string[] DefaultDeletePrefix = new[] { "Delete", "Remove" };
    /// <summary>
    /// 添加默认前后缀
    /// </summary>
    public DynamicApiConfig AddDefault()
    {
        // 配置可移除的前缀
        PrefixList.AddRange(DefaultPrefix);
        // 配置可移除的后缀
        SuffixList.AddRange(DefaultSuffix);
        AddDefaultMethodPrefix();
        return this;
    }

    /// <summary>
    /// 添加默认 method 识别前缀
    /// </summary>
    public DynamicApiConfig AddDefaultMethodPrefix()
    {
        GetPrefix.AddRange(DefaultGetPrefix);
        PostPrefix.AddRange(DefaultPostPrefix);
        PutPrefix.AddRange(DefaultPutPrefix);
        DeletePrefix.AddRange(DefaultDeletePrefix);
        GetPrefix = GetPrefix.Distinct().ToList();
        PostPrefix = PostPrefix.Distinct().ToList();
        PutPrefix = PutPrefix.Distinct().ToList();
        DeletePrefix = DeletePrefix.Distinct().ToList();

        return this;
    }

    /// <summary>
    /// 移除前缀
    /// </summary>
    public string RemovePrefix(string actionName)
    {
        foreach (var prefix in PrefixList)
        {
            if (actionName.StartsWith(prefix) && actionName != prefix)
                return actionName.Replace(prefix, string.Empty);
        }
        return actionName;
    }
    /// <summary>
    /// 移除后缀
    /// </summary>
    public string RemoveSuffix(string actionName)
    {
        foreach (var suffix in SuffixList)
        {
            if (actionName.EndsWith(suffix) && actionName != suffix)
                return actionName.Replace(suffix, string.Empty);
        }
        return actionName;
    }

    public string Remove(string actionName)
    {
        var temp = actionName;
        foreach (var prefix in PrefixList)
        {
            if (temp.StartsWith(prefix) && temp != prefix)
            {
                temp = temp.Replace(prefix, string.Empty);
                break;
            }
        }
        foreach (var suffix in SuffixList)
        {
            if (temp.EndsWith(suffix) && temp != suffix)
            {
                temp = temp.Replace(suffix, string.Empty);
                break;
            }
        }
        return temp;
    }
    /// <summary>
    /// 获取 http 类型
    /// </summary>
    public string GetHttpMethod(string actionName)
    {
        if (actionName.HasStartsWith(GetPrefix))
            return "GET";
        if (actionName.HasStartsWith(PutPrefix))
            return "PUT";
        if (actionName.HasStartsWith(DeletePrefix))
            return "DELETE";
        return "POST";
    }

    /// <summary>
    /// 构建 controller route
    /// </summary>
    /// <remarks>
    /// 如果配置了全局的路由前缀, 则需要将全局前缀拼接到原来的路由之前
    /// </remarks>
    public void ConfigControllerRoute(ControllerModel controller)
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
        if (GlobalPrefix.NotEmpty())
        {
            var newSelectors = controller.Selectors.Select(sel =>
            {
                var route = $@"{GlobalPrefix}/{sel.AttributeRouteModel.Template}";
                Console.WriteLine(@$"route= {route}");
                return new SelectorModel { AttributeRouteModel = new AttributeRouteModel(new RouteAttribute(route)), };
            }).ToList();
            controller.Selectors.Clear();
            controller.Selectors.AddRange(newSelectors);
        }
    }

    /// <summary>
    /// 构建 action route
    /// </summary>
    public void ConfigActionRoute(ActionModel action)
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
                selector.ActionConstraints.Add(new HttpMethodActionConstraint(new[] { GetHttpMethod(actionName) }));
            actionName = Remove(actionName);
            route.Append($"{(route.Length > 0 ? "/" : "")}{actionName}");

            selector.AttributeRouteModel = new AttributeRouteModel(new RouteAttribute(route.ToString()));
        }
    }
}