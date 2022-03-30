using System.Reflection;

namespace Collapsenav.Net.Tool;
public static partial class TypeExt
{
    /// <summary>
    /// 是否内建类型
    /// </summary>
    public static bool IsBaseType<T>(this T obj)
    {
        return typeof(T).IsBaseType();
    }
    /// <summary>
    /// 是否内建类型
    /// </summary>
    public static bool IsBuildInType<T>(this T obj)
    {
        return typeof(T).IsBaseType();
    }
    /// <summary>
    /// 是否内建类型
    /// </summary>
    public static bool IsBuildInType(this Type type)
    {
        return type.IsBaseType();
    }
    /// <summary>
    /// 是否内建类型
    /// </summary>
    public static bool IsBaseType(this Type type)
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
    /// 获取属性名称
    /// </summary>
    public static IEnumerable<string> PropNames<T>(this T obj)
    {
        return typeof(T).PropNames();
    }
    /// <summary>
    /// 获取属性名称
    /// </summary>
    public static IEnumerable<string> PropNames(this Type type)
    {
        return type.GetProperties().Select(item => item.Name);
    }
    /// <summary>
    /// 获取属性名称
    /// </summary>
    public static IEnumerable<string> PropNames<T>(this T obj, int depth)
    {
        return typeof(T).PropNames(depth);
    }
    /// <summary>
    /// 获取属性名称
    /// </summary>
    public static IEnumerable<string> PropNames(this Type type, int depth)
    {
        var props = type.GetProperties();
        var nameProps = props.Where(item => item.PropertyType.IsBuildInType());
        var loopProps = props.Where(item => !item.PropertyType.IsBuildInType());
        return depth > 0
            ? loopProps.Select(item => item.PropertyType.PropNames(depth - 1).Select(propName => $@"{item.Name}.{propName}")).Merge(nameProps.Select(item => item.Name))
            : nameProps.Select(item => item.Name).Concat(loopProps.Select(item => item.Name));
    }


    /// <summary>
    /// 获取内建类型的属性名
    /// </summary>
    public static IEnumerable<string> BuildInTypePropNames<T>(this T obj)
    {
        var nameProps = typeof(T).BuildInTypeProps();
        return nameProps.Select(item => item.Name);
    }
    /// <summary>
    /// 获取内建类型的属性名
    /// </summary>
    public static IEnumerable<string> BuildInTypePropNames(this Type type)
    {
        var nameProps = type.BuildInTypeProps();
        return nameProps.Select(item => item.Name);
    }


    /// <summary>
    /// 获取内建类型属性
    /// </summary>
    public static IEnumerable<PropertyInfo> BuildInTypeProps(this Type type)
    {
        return type.GetProperties().Where(item => item.PropertyType.IsBuildInType());
    }

    /// <summary>
    /// 获取T中存在E的attribute的属性及attribute值
    /// </summary>
    public static Dictionary<PropertyInfo, E> AttrValues<E>(this Type type) where E : Attribute
    {
        var props = type.Props().Where(item => item.CustomAttributes.Any(attr => attr.AttributeType == typeof(E)));
        var propData = props.Select(item => new KeyValuePair<PropertyInfo, E>(item, item.GetCustomAttribute<E>()))
        .ToDictionary(item => item.Key, item => item.Value);
        return propData;
    }

    /// <summary>
    /// 获取内建类型的属性和值
    /// </summary>
    public static Dictionary<PropertyInfo, object> BuildInTypeValues<T>(this T obj)
    {
        var propDict = typeof(T).BuildInTypeProps()
        .Select(item => new KeyValuePair<PropertyInfo, object>(item, obj.GetValue(item.Name)))
        .ToDictionary(item => item.Key, item => item.Value);
        return propDict;
    }
    /// <summary>
    /// 设置值
    /// </summary>
    public static void SetValue<T>(this T obj, string field, object value) where T : class
    {
        // 判断是否匿名类型, 可能会对性能有影响, 暂时不确定是否应该开启
        // var objType = obj.GetType();
        // if (objType.Name.Contains("AnonymousType"))
        //     obj.SetAnonymousValue(field, value);
        var fieldName = field.FirstTo('.');
        var prop = obj.GetType().GetProperty(fieldName);
        if (fieldName.Length == field.Length)
            prop?.SetValue(obj, value);
        else if (prop != null)
        {
            var propValue = obj.GetValue(fieldName);
            if (propValue == null)
            {
                propValue = Activator.CreateInstance(prop.PropertyType);
                obj.SetValue(fieldName, propValue);
            }
            propValue.SetValue(field.Last(field.Length - (fieldName.Length + 1)), value);
        }
    }

    /// <summary>
    /// 设置匿名对象的值
    /// </summary>
    public static void SetAnonymousValue(this object obj, string field, object value)
    {
        var fieldName = field.FirstTo('.');
        var runtimeField = obj.GetType().GetRuntimeFields().FirstOrDefault(item => item.Name == $"<{fieldName}>i__Field");
        if (fieldName == field)
            runtimeField?.SetValue(obj, value);
        else if (runtimeField != null)
            obj.GetValue(fieldName)?.SetAnonymousValue(field.Last(field.Length - (fieldName.Length + 1)), value);
    }

    /// <summary>
    /// 获取类型的属性
    /// </summary>
    public static IEnumerable<PropertyInfo> Props(this Type type)
    {
        return type.GetProperties();
    }
    /// <summary>
    /// 获取类型的属性
    /// </summary>
    public static IEnumerable<PropertyInfo> Props<T>(this T obj)
    {
        return typeof(T).GetProperties();
    }

    /// <summary>
    /// 就...GetValue
    /// </summary>
    /// <param name="obj">对象</param>
    /// <param name="field">属性/字段</param>
    public static object GetValue<T>(this T obj, string field)
    {
        var fieldName = field.FirstTo('.');
        var prop = obj.GetType().GetProperty(fieldName);
        if (fieldName.Length == field.Length)
            return prop?.GetValue(obj);
        else if (prop != null)
        {
            var propValue = prop?.GetValue(obj);
            return propValue?.GetValue(field.Last(field.Length - (fieldName.Length + 1)));
        }
        return null;
    }
    /// <summary>
    /// 比较两个对象
    /// </summary>
    public static Difference<T> Difference<T>(this T before, T end)
    {
        return new Difference<T>(before, end);
    }
}
