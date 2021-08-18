using System.Collections.Generic;
using System.Text.Json;

namespace Collapsenav.Net.Tool
{
    public static class StringExt
    {
        public static bool IsNull(this string str) => string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str);
        public static T ToObj<T>(this string str, JsonSerializerOptions options = null) => JsonSerializer.Deserialize<T>(str, options);
        public static IEnumerable<T> ToObjCollection<T>(this string str, JsonSerializerOptions options = null) => JsonSerializer.Deserialize<IEnumerable<T>>(str, options);
        public static string ToJson(this object obj, JsonSerializerOptions options = null) => JsonSerializer.Serialize(obj, options);
        public static string PadLeft(this int num, int total, char? fill) => fill.HasValue ? num.ToString().PadLeft(total, fill.Value) : num.ToString().PadLeft(total);
        public static string PadLeft(this double num, int total, char? fill) => fill.HasValue ? num.ToString().PadLeft(total, fill.Value) : num.ToString().PadLeft(total);
        public static string PadLeft(this long num, int total, char? fill) => fill.HasValue ? num.ToString().PadLeft(total, fill.Value) : num.ToString().PadLeft(total);
        public static string PadRight(this int num, int total, char? fill) => fill.HasValue ? num.ToString().PadRight(total, fill.Value) : num.ToString().PadRight(total);
        public static string PadRight(this double num, int total, char? fill) => fill.HasValue ? num.ToString().PadRight(total, fill.Value) : num.ToString().PadRight(total);
        public static string PadRight(this long num, int total, char? fill) => fill.HasValue ? num.ToString().PadRight(total, fill.Value) : num.ToString().PadRight(total);
    }
}
