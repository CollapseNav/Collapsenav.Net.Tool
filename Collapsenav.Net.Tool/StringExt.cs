using System.Collections.Generic;
using System.Text.Json;

namespace Collapsenav.Net.Tool
{
    public static class StringExt
    {
        public static bool IsNull(this string str) => string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str);
        public static string PadLeft(this int num, int total, char? fill) => fill.HasValue ? num.ToString().PadLeft(total, fill.Value) : num.ToString().PadLeft(total);
        public static string PadLeft(this double num, int total, char? fill) => fill.HasValue ? num.ToString().PadLeft(total, fill.Value) : num.ToString().PadLeft(total);
        public static string PadLeft(this long num, int total, char? fill) => fill.HasValue ? num.ToString().PadLeft(total, fill.Value) : num.ToString().PadLeft(total);
        public static string PadRight(this int num, int total, char? fill) => fill.HasValue ? num.ToString().PadRight(total, fill.Value) : num.ToString().PadRight(total);
        public static string PadRight(this double num, int total, char? fill) => fill.HasValue ? num.ToString().PadRight(total, fill.Value) : num.ToString().PadRight(total);
        public static string PadRight(this long num, int total, char? fill) => fill.HasValue ? num.ToString().PadRight(total, fill.Value) : num.ToString().PadRight(total);
    }
}
