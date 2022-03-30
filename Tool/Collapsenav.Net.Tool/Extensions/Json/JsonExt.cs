using System.Text.Json;
using System.Text.Json.Serialization;

namespace Collapsenav.Net.Tool;
public static partial class JsonExt
{
    public static JsonSerializerOptions DefaultJsonSerializerOption = new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
    };
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
}
