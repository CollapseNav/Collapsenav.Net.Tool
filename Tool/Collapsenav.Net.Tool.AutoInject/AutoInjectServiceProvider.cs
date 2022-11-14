using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Collapsenav.Net.Tool.AutoInject;

public class AutoInjectServiceProvider : IServiceProvider, ISupportRequiredService
{
    private readonly IServiceProvider _provider;
    public AutoInjectServiceProvider(IServiceProvider provider) { _provider = provider; }
    public object GetRequiredService(Type serviceType) => GetService(serviceType);
    public object GetService(Type serviceType)
    {
        var obj = _provider.GetService(serviceType);
        if (obj == null) return null;
        var type = obj.GetType();
        // 获取被标记了自动注入的属性
        var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
        .Where(prop => prop.GetCustomAttribute<AutoInjectAttribute>() != null);
        // 为属性注入对象
        foreach (var prop in props)
            prop.SetValue(obj, GetService(prop.PropertyType));
        // 获取被标记了自动注入的字段
        var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance)
        .Where(prop => prop.GetCustomAttribute<AutoInjectAttribute>() != null);
        // 为字段注入对象
        foreach (var field in fields)
            field.SetValue(obj, GetService(field.FieldType));
        return obj;
    }
}