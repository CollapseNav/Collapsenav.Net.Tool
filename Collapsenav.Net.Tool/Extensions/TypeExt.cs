using System;
using System.Collections.Generic;

namespace Collapsenav.Net.Tool
{
    public static class TypeExt
    {
        public static bool IsBuildInType<T>(this T obj)
        {
            return TypeTool.IsBuildInType<T>();
        }
        public static bool IsBuildInType(this Type obj)
        {
            return TypeTool.IsBuildInType(obj);
        }
        public static IEnumerable<string> PropNames<T>(this T obj)
        {
            return TypeTool.PropNames<T>();
        }
        public static IEnumerable<string> PropNames(this Type obj)
        {
            return TypeTool.PropNames(obj);
        }
        public static IEnumerable<string> PropNames<T>(this T obj, int depth)
        {
            return TypeTool.PropNames<T>(depth);
        }
        public static IEnumerable<string> PropNames(this Type obj, int depth)
        {
            return TypeTool.PropNames(obj, depth);
        }
    }
}
