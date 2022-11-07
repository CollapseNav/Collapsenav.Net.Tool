using System.Reflection;

namespace Collapsenav.Net.Tool;

public static partial class AttributeExt
{
    /// <summary>
    /// type 拥有对应的attribute类型
    /// </summary>
    public static bool HasAttribute<T>(this Type type)
    {
        return type.HasAttribute(typeof(T));
    }
    /// <summary>
    /// type 拥有对应的attribute类型
    /// </summary>
    public static bool HasAttribute(this Type type, params Type[] attrTypes)
    {
        return type.CustomAttributes.Any(attr => attrTypes.Contains(attr.AttributeType));
    }
    /// <summary>
    /// 成员 member 拥有对应的attribute类型
    /// </summary>
    public static bool HasAttribute<T>(this MemberInfo info)
    {
        return info.HasAttribute(typeof(T));
    }
    /// <summary>
    /// 成员 member 拥有对应的attribute类型
    /// </summary>
    public static bool HasAttribute(this MemberInfo info, params Type[] attrTypes)
    {
        return info.CustomAttributes.Any(attr => attrTypes.Contains(attr.AttributeType));
    }
    /// <summary>
    /// 拥有对应的attribute类型
    /// </summary>
    public static IEnumerable<PropertyInfo> HasAttrProps(this Type type, Type attrType)
    {
        return type.Props().Where(item => item.HasAttribute(attrType));
    }
    /// <summary>
    /// 拥有对应的attribute类型
    /// </summary>
    public static IEnumerable<PropertyInfo> HasAttrProps<E>(this Type type) where E : Attribute
    {
        return type.HasAttrProps(typeof(E));
    }
    /// <summary>
    /// 拥有对应的attribute类型
    /// </summary>
    public static IEnumerable<FieldInfo> HasAttrFields(this Type type, Type attrType)
    {
        return type.GetFields().Where(item => item.HasAttribute(attrType));
    }
    /// <summary>
    /// 拥有对应的attribute类型
    /// </summary>
    public static IEnumerable<FieldInfo> HasAttrFields<E>(this Type type) where E : Attribute
    {
        return type.HasAttrFields(typeof(E));
    }
    /// <summary>
    /// 拥有对应的attribute类型
    /// </summary>
    public static IEnumerable<MemberInfo> HasAttrMembers(this Type type, Type attrType)
    {
        return type.GetMembers().Where(item => item.HasAttribute(attrType));
    }
    /// <summary>
    /// 拥有对应的attribute类型
    /// </summary>
    public static IEnumerable<MemberInfo> HasAttrMembers<E>(this Type type) where E : Attribute
    {
        return type.HasAttrMembers(typeof(E));
    }


    /// <summary>
    /// 获取T中存在E的attribute的属性及attribute值
    /// </summary>
    public static Dictionary<PropertyInfo, E> AttrValues<E>(this Type type) where E : Attribute
    {
        return type.AttrPropValues<E>();
    }
    /// <summary>
    /// 获取T中存在E的attribute的属性及attribute值
    /// </summary>
    public static Dictionary<PropertyInfo, E> AttrPropValues<E>(this Type type) where E : Attribute
    {
        var props = type.HasAttrProps<E>();
        var propData = props.Select(item => new KeyValuePair<PropertyInfo, E>(item, item.GetCustomAttribute<E>()))
        .ToDictionary(item => item.Key, item => item.Value);
        return propData;
    }
    /// <summary>
    /// 获取T中存在E的attribute的属性及attribute值
    /// </summary>
    public static Dictionary<FieldInfo, E> AttrFieldValues<E>(this Type type) where E : Attribute
    {
        var fields = type.HasAttrFields<E>();
        var fieldData = fields.Select(item => new KeyValuePair<FieldInfo, E>(item, item.GetCustomAttribute<E>()))
        .ToDictionary(item => item.Key, item => item.Value);
        return fieldData;
    }

    public static Dictionary<MemberInfo, E> AttrMemberValues<E>(this Type type) where E : Attribute
    {
        var props = type.AttrPropValues<E>();
        var fields = type.AttrFieldValues<E>();
        Dictionary<MemberInfo, E> result = new();
        if (props is IDictionary<PropertyInfo, E> propDict && propDict.NotEmpty())
            propDict.ForEach(dict => result.AddOrUpdate(dict.Key, dict.Value));
        if (fields is IDictionary<FieldInfo, E> fieldDict && fieldDict.NotEmpty())
            fieldDict.ForEach(dict => result.AddOrUpdate(dict.Key, dict.Value));
        return result;
    }

    public static IEnumerable<Type> AttrPropTypes(this Type type, Type attrType)
    {
        var props = type.HasAttrProps(attrType);
        return props.Select(prop => prop.PropertyType);
    }
    public static IEnumerable<Type> AttrPropTypes<E>(this Type type) where E : Attribute
    {
        return type.AttrPropTypes(typeof(E));
    }
    public static IEnumerable<Type> AttrFieldTypes(this Type type, Type attrType)
    {
        var fields = type.HasAttrFields(attrType);
        return fields.Select(field => field.FieldType);
    }
    public static IEnumerable<Type> AttrFieldTypes<E>(this Type type) where E : Attribute
    {
        return type.AttrFieldTypes(typeof(E));
    }
    public static IEnumerable<Type> AttrMemberTypes(this Type type, Type attrType)
    {
        var props = type.HasAttrProps(attrType);
        var fields = type.HasAttrFields(attrType);
        return props.Select(prop => prop.PropertyType).AddRange(fields.Select(field => field.FieldType));
    }
    public static IEnumerable<Type> AttrMemberTypes<E>(this Type type) where E : Attribute
    {
        return type.AttrMemberTypes(typeof(E));
    }

}