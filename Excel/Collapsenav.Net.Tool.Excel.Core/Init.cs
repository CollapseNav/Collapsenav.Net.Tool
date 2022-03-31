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
        newAsses.ForEach(item => AppDomain.CurrentDomain.Load(item.FullName));
        var types = newAsses.SelectMany(s => s.GetTypes().Where(item => item.IsClass && item.IsPublic && item.HasGenericInterface(typeof(IExcelContainer<>))));

        types.ForEach(item =>
        {
            try
            {
                Activator.CreateInstance(item);
            }
            catch { }
        });
    }
#endif
}