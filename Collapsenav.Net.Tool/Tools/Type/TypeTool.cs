using System.Reflection;

namespace Collapsenav.Net.Tool;
public partial class TypeTool
{
    /// <summary>
    /// 是否内建(基本)类型
    /// </summary>
    public static bool IsBuildInType(Type type)
    {
        return type.Name switch
        {
            nameof(Boolean) => true,
            nameof(Byte) => true,
            nameof(SByte) => true,
            nameof(Char) => true,
            nameof(Decimal) => true,
            nameof(Double) => true,
            nameof(Single) => true,
            nameof(Int32) => true,
            nameof(UInt32) => true,
            nameof(IntPtr) => true,
            nameof(UIntPtr) => true,
            nameof(Int64) => true,
            nameof(UInt64) => true,
            nameof(Int16) => true,
            nameof(UInt16) => true,
            nameof(String) => true,
            nameof(DateTime) => true,
            nameof(Guid) => true,
            _ => false,
        };
    }
    /// <summary>
    /// 是否内建(基本)类型
    /// </summary>
    public static bool IsBuildInType<T>()
    {
        return IsBuildInType(typeof(T));
    }
    /// <summary>
    /// 是否内建(基本)类型
    /// </summary>
    public static bool IsBaseType<T>()
    {
        return IsBuildInType(typeof(T));
    }
    /// <summary>
    /// 是否内建(基本)类型
    /// </summary>
    public static bool IsBaseType(Type type)
    {
        return IsBuildInType(type);
    }

    /// <summary>
    /// 获取属性名称
    /// </summary>
    public static IEnumerable<string> PropNames(Type type)
    {
        return type.GetProperties().Select(item => item.Name);
    }
    /// <summary>
    /// 获取属性名称
    /// </summary>
    public static IEnumerable<string> PropNames<T>()
    {
        return PropNames(typeof(T));
    }
    /// <summary>
    /// 获取属性名称
    /// </summary>
    public static IEnumerable<string> PropNames(Type type, int depth)
    {
        var props = type.GetProperties();
        var nameProps = props.Where(item => item.PropertyType.IsBuildInType());
        var loopProps = props.Where(item => !item.PropertyType.IsBuildInType());
        if (depth > 0)
            return loopProps.Select(item => PropNames(item.PropertyType, depth - 1).Select(propName => $@"{item.Name}.{propName}")).Merge(nameProps.Select(item => item.Name));
        return nameProps.Select(item => item.Name).Concat(loopProps.Select(item => item.Name));
    }
    /// <summary>
    /// 获取属性名称
    /// </summary>
    public static IEnumerable<string> PropNames<T>(int? depth)
    {
        return PropNames(typeof(T), depth ?? 0);
    }

    /// <summary>
    /// 获取内建类型的属性名
    /// </summary>
    public static IEnumerable<string> BuildInTypePropNames<T>()
    {
        return BuildInTypePropNames(typeof(T));
    }

    /// <summary>
    /// 获取内建类型的属性名
    /// </summary>
    public static IEnumerable<string> BuildInTypePropNames(Type type)
    {
        var nameProps = BuildInTypeProps(type);
        return nameProps.Select(item => item.Name);
    }
    /// <summary>
    /// 就...GetValue
    /// </summary>
    /// <param name="obj">对象</param>
    /// <param name="field">属性/字段</param>
    public static object GetValue<T>(object obj, string field)
    {
        var prop = typeof(T).GetProperties().Where(item => item.Name == field && item.PropertyType.IsBuildInType()).FirstOrDefault();
        if (prop == null)
            return "";
        return prop.GetValue(obj);
    }

    /// <summary>
    /// 获取类型的属性
    /// </summary>
    public static IEnumerable<PropertyInfo> Props(Type type)
    {
        return type.GetProperties();
    }
    /// <summary>
    /// 获取类型的属性
    /// </summary>
    public static IEnumerable<PropertyInfo> Props<T>()
    {
        return typeof(T).GetProperties();
    }

    /// <summary>
    /// 获取内建类型属性
    /// </summary>
    public static IEnumerable<PropertyInfo> BuildInTypeProps(Type type)
    {
        return type.GetProperties().Where(item => IsBuildInType(item.PropertyType));
    }

    /// <summary>
    /// 获取内建类型属性
    /// </summary>
    public static IEnumerable<PropertyInfo> BuildInTypeProps<T>()
    {
        return BuildInTypeProps(typeof(T));
    }

    /// <summary>
    /// 获取内建类型的属性和值
    /// </summary>
    public static Dictionary<PropertyInfo, object> BuildInTypeValues<T>(object obj)
    {
        var propDict = BuildInTypeProps<T>()
        .Select(item => new KeyValuePair<PropertyInfo, object>(item, GetValue<T>(obj, item.Name)))
        .ToDictionary(item => item.Key, item => item.Value);
        return propDict;
    }

    /// <summary>
    /// 获取 T 中拥有 E(Attribute) 的属性
    /// </summary>
    /// <typeparam name="T">Class</typeparam>
    /// <typeparam name="E">Attribute</typeparam>
    public static Dictionary<PropertyInfo, E> AttrValues<T, E>() where E : Attribute
    {
        var props = BuildInTypeProps<T>().Where(item => item.CustomAttributes.Any(attr => attr.AttributeType == typeof(E)));
        var propData = props.Select(item => new KeyValuePair<PropertyInfo, E>(item, item.GetCustomAttribute<E>()))
        .ToDictionary(item => item.Key, item => item.Value);
        return propData;
    }

    /// <summary>
    /// 获取T中存在E的attribute的属性及attribute值
    /// </summary>
    public static Dictionary<PropertyInfo, E> AttrValues<E>(Type T) where E : Attribute
    {
        var props = BuildInTypeProps(T).Where(item => item.CustomAttributes.Any(attr => attr.AttributeType == typeof(E)));
        var propData = props.Select(item => new KeyValuePair<PropertyInfo, E>(item, item.GetCustomAttribute<E>()))
        .ToDictionary(item => item.Key, item => item.Value);
        return propData;
    }
}
