using System.Collections.Generic;
using System.Text.Json;

namespace Collapsenav.Net.Tool
{
    public static class JsonExt
    {
        public static T ToObj<T>(this string str, JsonSerializerOptions options = null) => JsonSerializer.Deserialize<T>(str, options);
        public static IEnumerable<T> ToObjCollection<T>(this string str, JsonSerializerOptions options = null) => JsonSerializer.Deserialize<IEnumerable<T>>(str, options);
        public static string ToJson(this object obj, JsonSerializerOptions options = null) => JsonSerializer.Serialize(obj, options);
    }
}
