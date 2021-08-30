using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Collapsenav.Net.Tool
{
    public static class JsonExt
    {
        public static JsonSerializerOptions DefaultJsonSerializerOption = new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        /// <summary>
        /// Json字符串转为对象
        /// </summary>
        public static T ToObj<T>(this string str, JsonSerializerOptions options = null) => JsonSerializer.Deserialize<T>(str, options ?? DefaultJsonSerializerOption);
        /// <summary>
        /// Json字符串转为对象集合
        /// </summary>
        public static IEnumerable<T> ToObjCollection<T>(this string str, JsonSerializerOptions options = null) => JsonSerializer.Deserialize<IEnumerable<T>>(str, options ?? DefaultJsonSerializerOption);
        /// <summary>
        /// 对象转为Json字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string ToJson(this object obj, JsonSerializerOptions options = null) => JsonSerializer.Serialize(obj, options ?? DefaultJsonSerializerOption);
    }
}
