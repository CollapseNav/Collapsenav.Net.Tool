using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace Collapsenav.Net.Tool;

public class JsonOptions
{
    /// <summary>
    /// 大小不敏感, 驼峰命名, 忽略循环引用, Encoder all all 叫
    /// </summary>
    public static readonly JsonSerializerOptions BetterUseOption = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
    };

    /// <summary>
    /// 彻底的默认配置(非常不推荐使用)
    /// </summary>
    public static readonly JsonSerializerOptions DefaultOption = new();
}