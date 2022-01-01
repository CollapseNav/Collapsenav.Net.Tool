using System.Reflection;

namespace Collapsenav.Net.Tool;
public static partial class TypeExt
{
    /// <summary>
    /// 是否内建类型
    /// </summary>
    public static bool IsBuildInType<T>(this T obj)
    {
        return TypeTool.IsBuildInType<T>();
    }
    /// <summary>
    /// 是否内建类型
    /// </summary>
    public static bool IsBuildInType(this Type obj)
    {
        return TypeTool.IsBuildInType(obj);
    }
    /// <summary>
    /// 是否内建类型
    /// </summary>
    public static bool IsBaseType(this Type obj)
    {
        return TypeTool.IsBaseType(obj);
    }
    /// <summary>
    /// 是否内建类型
    /// </summary>
    public static bool IsBaseType<T>(this T obj)
    {
        return TypeTool.IsBaseType<T>();
    }


    /// <summary>
    /// 获取属性名称
    /// </summary>
    public static IEnumerable<string> PropNames<T>(this T obj)
    {
        return TypeTool.PropNames<T>();
    }
    /// <summary>
    /// 获取属性名称
    /// </summary>
    public static IEnumerable<string> PropNames(this Type obj)
    {
        return TypeTool.PropNames(obj);
    }
    /// <summary>
    /// 获取属性名称
    /// </summary>
    public static IEnumerable<string> PropNames<T>(this T obj, int depth)
    {
        return TypeTool.PropNames<T>(depth);
    }
    /// <summary>
    /// 获取属性名称
    /// </summary>
    public static IEnumerable<string> PropNames(this Type obj, int depth)
    {
        return TypeTool.PropNames(obj, depth);
    }
    /// <summary>
    /// 就...GetValue
    /// </summary>
    /// <param name="obj">对象</param>
    /// <param name="field">属性/字段</param>
    public static object GetValue<T>(this T obj, string field)
    {
        return TypeTool.GetValue<T>(obj, field);
    }
    /// <summary>
    /// 获取内建类型的属性名
    /// </summary>
    public static IEnumerable<string> BuildInTypePropNames<T>(this T obj)
    {
        return TypeTool.BuildInTypePropNames<T>();
    }
    /// <summary>
    /// 获取内建类型的属性名
    /// </summary>
    public static IEnumerable<string> BuildInTypePropNames(this Type type)
    {
        return TypeTool.BuildInTypePropNames(type);
    }

    /// <summary>
    /// 获取T中存在E的attribute的属性及attribute值
    /// </summary>
    public static Dictionary<PropertyInfo, E> AttrValues<E>(this Type type) where E : Attribute
    {
        return TypeTool.AttrValues<E>(type);
    }

    public static void SetValue<T>(this T obj, string field, object value)
    {
        typeof(T).GetProperty(field)?.SetValue(obj, value);
    }
}
