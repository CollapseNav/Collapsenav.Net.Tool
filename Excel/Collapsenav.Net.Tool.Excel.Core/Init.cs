using System.Reflection;
using System.Runtime.CompilerServices;

namespace Collapsenav.Net.Tool.Excel;
public static class Init
{
#if NETCOREAPP
    [ModuleInitializer]
    public static void InitCellReader()
    {
        var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "Collapsenav.Net.Tool.Excel.*.dll");
        if (files.IsEmpty())
            return;
        var newAsses = files.Select(item => Assembly.LoadFrom(item)).ToList();
        var types = newAsses.SelectMany(ass => ass.GetTypes().Where(type => !type.IsInterface && type.Name == "Init"));
        types.ForEach(item =>
        {
            try
            {
                item.GetMethod("InitTypeSelector")?.Invoke(null, null);
            }
            catch { }
        });
    }
#endif
}