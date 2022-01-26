namespace Collapsenav.Net.Tool;
public static partial class StringExt
{
    /// <summary>
    /// 存在以啥啥啥开头
    /// </summary>
    public static bool HasStartsWith(this string input, params string[] filters) => filters.Any(item => input.StartsWith(item));
    /// <summary>
    /// 存在以啥啥啥开头
    /// </summary>
    public static bool HasStartsWith(this string input, IEnumerable<string> filters, bool ignoreCase = false) => filters.Any(item => input.StartsWith(item, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal));
    /// <summary>
    /// 存在以啥啥啥开头
    /// </summary>
    public static bool HasStartsWith(this IEnumerable<string> input, string filter, bool ignoreCase = false) => input.Any(item => item.StartsWith(filter, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal));
    /// <summary>
    /// 存在以啥啥啥结尾
    /// </summary>
    public static bool HasEndsWith(this string input, params string[] filters) => filters.Any(item => input.EndsWith(item));
    /// <summary>
    /// 存在以啥啥啥结尾
    /// </summary>
    public static bool HasEndsWith(this string input, IEnumerable<string> filters, bool ignoreCase = false) => filters.Any(item => input.EndsWith(item, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal));
    /// <summary>
    /// 存在以啥啥啥结尾
    /// </summary>
    public static bool HasEndsWith(this IEnumerable<string> input, string filter, bool ignoreCase = false) => input.Any(item => item.EndsWith(filter, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal));
    /// <summary>
    /// 全部以啥啥啥开头
    /// </summary>
    public static bool AllStartsWith(this string input, params string[] filters) => filters.All(item => input.StartsWith(item));
    /// <summary>
    /// 存在以啥啥啥开头
    /// </summary>
    public static bool AllStartsWith(this string input, IEnumerable<string> filters, bool ignoreCase = false) => filters.All(item => input.StartsWith(item, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal));
    /// <summary>
    /// 全部以啥啥啥开头
    /// </summary>
    public static bool AllStartsWith(this IEnumerable<string> input, string filter, bool ignoreCase = false) => input.All(item => item.StartsWith(filter, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal));
    /// <summary>
    /// 全部以啥啥啥结尾
    /// </summary>
    public static bool AllEndsWith(this string input, params string[] filters) => filters.All(item => input.EndsWith(item));
    /// <summary>
    /// 存在以啥啥啥结尾
    /// </summary>
    public static bool AllEndsWith(this string input, IEnumerable<string> filters, bool ignoreCase = false) => filters.All(item => input.EndsWith(item, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal));
    /// <summary>
    /// 全部以啥啥啥结尾
    /// </summary>
    public static bool AllEndsWith(this IEnumerable<string> input, string filter, bool ignoreCase = false) => input.All(item => item.EndsWith(filter, ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal));
}