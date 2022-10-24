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
        var props = serviceType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
        .Where(prop => prop.GetCustomAttribute<AutoInjectAttribute>() != null);
        foreach (var prop in props)
            prop.SetValue(obj, GetService(prop.PropertyType));
        var fields = serviceType.GetFields(BindingFlags.Public | BindingFlags.Instance)
        .Where(prop => prop.GetCustomAttribute<AutoInjectAttribute>() != null);
        foreach (var field in fields)
            field.SetValue(obj, GetService(field.FieldType));
        return obj;
    }
}