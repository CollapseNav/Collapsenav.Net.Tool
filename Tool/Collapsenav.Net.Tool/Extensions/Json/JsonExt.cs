using System.Text.Json;
using System.Text.Json.Nodes;

namespace Collapsenav.Net.Tool;
public static partial class JsonExt
{
    public static JsonSerializerOptions DefaultJsonSerializerOption = JsonOptions.BetterUseOption;
    /// <summary>
    /// Json字符串转为对象
    /// </summary>
    public static T ToObj<T>(this string str, JsonSerializerOptions options = null) => JsonSerializer.Deserialize<T>(str, options ?? DefaultJsonSerializerOption);
    public static object ToObj(this string str, Type type, JsonSerializerOptions options = null) => JsonSerializer.Deserialize(str, type, options ?? DefaultJsonSerializerOption);
    /// <summary>
    /// Json字符串转为对象集合
    /// </summary>
    public static IEnumerable<T> ToObjCollection<T>(this string str, JsonSerializerOptions options = null) => JsonSerializer.Deserialize<IEnumerable<T>>(str, options ?? DefaultJsonSerializerOption);
    /// <summary>
    /// 对象转为Json字符串
    /// </summary>
    public static string ToJson<T>(this T obj, JsonSerializerOptions options = null) => JsonSerializer.Serialize(obj, options ?? DefaultJsonSerializerOption);



    /// <summary>
    /// 通过json的序列化反序列化map对象
    /// </summary>
    public static T JsonMap<T>(this object obj) => obj.ToJson().ToObj<T>();
    /// <summary>
    /// 通过json的序列化反序列化map对象
    /// </summary>
    public static T JsonMap<S, T>(this S obj) => obj.ToJson().ToObj<T>();

    /// <summary>
    /// 通过json的序列化反序列化将左边的对象映射到右边
    /// </summary>
    public static T JsonMap<S, T>(this S origin, T target) where S : class where T : class
    {
        var propNames = origin.PropNames();
        var temp = target.ToJson().ToObj<T>();
        foreach (var name in propNames)
            temp.SetValue(name, origin.GetValue(name));
        return temp;
    }

    /// <summary>
    /// 简单使用Json的序列化反序列化进行copy
    /// </summary>
    public static T JsonCopy<T>(this T origin)
    {
        return origin.ToJson().ToObj<T>();
    }

    /// <summary>
    /// 从文件中读取 JsonElement
    /// </summary>
    public static JsonElement GetJsonElementFromPath(this string path) => path.GetJsonDocumentFromPath().RootElement;
    /// <summary>
    /// 从文件中读取 JsonElement
    /// </summary>
    public static async ValueTask<JsonElement> GetJsonElementFromPathAsync(this string path) => (await path.GetJsonDocumentFromPathAsync()).RootElement;
    /// <summary>
    /// 从文件中读取 JsonDocument
    /// </summary>
    public static JsonDocument GetJsonDocumentFromPath(this string path)
    {
        using var stream = path.OpenReadWriteShareStream();
        return JsonDocument.Parse(stream);
    }
    /// <summary>
    /// 从文件中读取 JsonDocument
    /// </summary>
    public static async ValueTask<JsonDocument> GetJsonDocumentFromPathAsync(this string path)
    {
        using var stream = path.OpenReadWriteShareStream();
        return await JsonDocument.ParseAsync(stream);
    }
    /// <summary>
    /// 从文件中读取 JsonObject
    /// </summary>
    public static JsonObject GetJsonObjectFromPath(this string path) => JsonObject.Create(path.GetJsonElementFromPath());
    /// <summary>
    /// 从文件中读取 JsonObject
    /// </summary>
    public static async ValueTask<JsonObject> GetJsonObjectFromPathAsync(this string path) => JsonObject.Create(await path.GetJsonElementFromPathAsync());
    /// <summary>
    /// 将json字符串转为 JsonDocument
    /// </summary>
    public static JsonDocument ToJsonDocument(this string jsonStr)
    {
        return JsonDocument.Parse(jsonStr);
    }
    /// <summary>
    /// 将json字符串转为 JsonElement
    /// </summary>
    public static JsonElement ToJsonElement(this string jsonStr)
    {
        return JsonDocument.Parse(jsonStr).RootElement;
    }
    /// <summary>
    /// 将json字符串转为 JsonNode
    /// </summary>
    public static JsonNode ToJsonNode(this string jsonStr)
    {
        return JsonNode.Parse(jsonStr);
    }
    /// <summary>
    /// 将json字符串转为 JsonObject
    /// </summary>
    public static JsonObject ToJsonObject(this string jsonStr)
    {
        return JsonObject.Create(jsonStr.ToJsonElement());
    }
    /// <summary>
    /// 将json字符串转为 JsonArray
    /// </summary>
    public static JsonArray ToJsonArray(this string jsonStr)
    {
        return JsonArray.Create(jsonStr.ToJsonElement());
    }
    /// <summary>
    /// 将jsonnode转为指定对象
    /// </summary>
    public static T ToObj<T>(this JsonNode node, JsonSerializerOptions options = null) => JsonSerializer.Deserialize<T>(node, options ?? DefaultJsonSerializerOption);
}
