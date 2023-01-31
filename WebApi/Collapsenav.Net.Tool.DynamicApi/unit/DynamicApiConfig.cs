namespace Collapsenav.Net.Tool.DynamicApi;

public class DynamicApiConfig
{
    public string GlobalPrefix { get; set; } = "api";
    public List<string> PrefixList { get; set; } = new();
    public List<string> SuffixList { get; set; } = new();

    public List<string> GetPrefix { get; set; } = new();
    public List<string> PostPrefix { get; set; } = new();
    public List<string> PutPrefix { get; set; } = new();
    public List<string> DeletePrefix { get; set; } = new();

    public string[] DefaultPrefix = new[] { "Get", "Query", "Post", };
    public string[] DefaultSuffix = new[] { "Async" };
    public string[] DefaultGetPrefix = new[] { "Get", "Query" };
    public string[] DefaultPostPrefix = new[] { "Add", "Create", "Post" };
    public string[] DefaultPutPrefix = new[] { "Put", "Update" };
    public string[] DefaultDeletePrefix = new[] { "Delete", "Remove" };
    /// <summary>
    /// 添加默认前后缀
    /// </summary>
    public void AddDefault()
    {
        // 配置可移除的前缀
        PrefixList.AddRange(DefaultPrefix);
        // 配置可移除的后缀
        SuffixList.AddRange(DefaultSuffix);
        AddDefaultMethodPrefix();
    }

    /// <summary>
    /// 添加默认 method 识别前缀
    /// </summary>
    public void AddDefaultMethodPrefix()
    {
        GetPrefix.AddRange(DefaultGetPrefix);
        PostPrefix.AddRange(DefaultPostPrefix);
        PutPrefix.AddRange(DefaultPutPrefix);
        DeletePrefix.AddRange(DefaultDeletePrefix);
        GetPrefix = GetPrefix.Distinct().ToList();
        PostPrefix = PostPrefix.Distinct().ToList();
        PutPrefix = PutPrefix.Distinct().ToList();
        DeletePrefix = DeletePrefix.Distinct().ToList();
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
}