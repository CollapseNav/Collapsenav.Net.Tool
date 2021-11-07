using System;
using System.Collections.Generic;

namespace Collapsenav.Net.Tool
{
    public static partial class TypeExt
    {
        public static bool IsBuildInType<T>(this T obj)
        {
            return TypeTool.IsBuildInType<T>();
        }
        public static bool IsBuildInType(this Type obj)
        {
            return TypeTool.IsBuildInType(obj);
        }
        public static bool IsBaseType(this Type obj)
        {
            return TypeTool.IsBaseType(obj);
        }
        public static bool IsBaseType<T>(this T obj)
        {
            return TypeTool.IsBaseType<T>();
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
        public static object GetValue<T>(this T obj, string field)
        {
            return TypeTool.GetValue<T>(obj, field);
        }
        public static IEnumerable<string> BuildInTypePropNames<T>(this T obj)
        {
            return TypeTool.BuildInTypePropNames<T>();
        }
        public static IEnumerable<string> BuildInTypePropNames(this Type type)
        {
            return TypeTool.BuildInTypePropNames(type);
        }
    }
}
