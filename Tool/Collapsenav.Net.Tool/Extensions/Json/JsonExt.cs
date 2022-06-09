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
    /// <param name="obj"></param>
    /// <param name="options"></param>
    public static string ToJson<T>(this T obj, JsonSerializerOptions options = null) => JsonSerializer.Serialize(obj, options ?? DefaultJsonSerializerOption);

    /// <summary>
    /// 通过json的序列化反序列化map对象
    /// </summary>
    public static T JsonMap<T>(this object obj) => obj.ToJson().ToObj<T>();
    /// <summary>
    /// 通过json的序列化反序列化map对象
    /// </summary>
    public static T JsonMap<S, T>(this S obj) => obj.ToJson().ToObj<T>();

    public static T JsonMap<S, T>(this S origin, T target) where S : class where T : class
    {
        var propNames = origin.PropNames();
        var temp = target.ToJson().ToObj<T>();
        foreach (var name in propNames)
            temp.SetValue(name, origin.GetValue(name));
        return temp;
    }

    public static JsonElement GetJsonElement(this string path) => path.GetJsonDocument().RootElement;
    public static async ValueTask<JsonElement> GetJsonElementAsync(this string path) => (await path.GetJsonDocumentAsync()).RootElement;
    public static JsonDocument GetJsonDocument(this string path)
    {
        using var stream = path.OpenReadWriteShareStream();
        return JsonDocument.Parse(stream);
    }
    public static async ValueTask<JsonDocument> GetJsonDocumentAsync(this string path)
    {
        using var stream = path.OpenReadWriteShareStream();
        return await JsonDocument.ParseAsync(stream);
    }
    public static JsonDocument ToJsonDocument(this string jsonStr)
    {
        return JsonDocument.Parse(jsonStr);
    }
    public static JsonElement ToJsonElement(this string jsonStr)
    {
        return JsonDocument.Parse(jsonStr).RootElement;
    }
    public static JsonObject GetJsonObject(this string path) => JsonObject.Create(path.GetJsonElement());
    public static async ValueTask<JsonObject> GetJsonObjectAsync(this string path) => JsonObject.Create(await path.GetJsonElementAsync());
}
