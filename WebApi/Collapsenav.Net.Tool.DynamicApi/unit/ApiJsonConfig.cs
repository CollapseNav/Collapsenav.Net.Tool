using Microsoft.AspNetCore.Components;

namespace Collapsenav.Net.Tool.DynamicApi;

public class ApiJsonConfig
{
    public string ApiGet { get; set; }
    public string ApiPost { get; set; }
    public string ApiPut { get; set; }
    public string ApiDelete { get; set; }
    public string RemovePrefix { get; set; }
    public string RemoveSuffix { get; set; }

    public string GlobalPrefix { get; set; }

    /// <summary>
    /// 获取默认apiconfig
    /// </summary>
    public static DynamicApiConfig BuildDefaultApiConfig()
    {
        var config = new DynamicApiConfig();
        return config.AddDefault();
    }
    /// <summary>
    /// 根据配置生成apiconfig
    /// </summary>
    public DynamicApiConfig BuildApiConfig()
    {
        var config = new DynamicApiConfig();
        var splitArray = new[] { ',', '|' };
        if (ApiGet.NotEmpty())
            config.GetPrefix.AddRange(ApiGet.Split(splitArray));
        if (ApiPost.NotEmpty())
            config.PostPrefix.AddRange(ApiPost.Split(splitArray));
        if (ApiPut.NotEmpty())
            config.PutPrefix.AddRange(ApiPut.Split(splitArray));
        if (ApiDelete.NotEmpty())
            config.DeletePrefix.AddRange(ApiDelete.Split(splitArray));
        if (RemovePrefix.NotEmpty())
            config.PrefixList.AddRange(RemovePrefix.Split(splitArray));
        if (RemoveSuffix.NotEmpty())
            config.SuffixList.AddRange(RemoveSuffix.Split(splitArray));

        config.GlobalPrefix = GlobalPrefix;

        return config;
    }
}