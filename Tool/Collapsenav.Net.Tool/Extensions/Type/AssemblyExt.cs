using System.Reflection;

namespace Collapsenav.Net.Tool;

public static partial class AssemblyExt
{
    /// <summary>
    /// 判断是否自定义程序集(通过publickeytoken是否为空判断)
    /// </summary>
    /// <param name="ass"></param>
    public static bool IsCustomerAssembly(this AssemblyName ass)
    {
        return ass.GetPublicKeyToken().IsEmpty();
    }
    /// <summary>
    /// 判断是否自定义程序集(通过publickeytoken是否为空判断)
    /// </summary>
    /// <param name="ass"></param>
    public static bool IsCustomerAssembly(this Assembly ass)
    {
        return ass.GetName().IsCustomerAssembly() && !ass.IsDynamic;
    }
    /// <summary>
    /// 获取所有assemblyName
    /// </summary>
    public static IEnumerable<AssemblyName> GetAllAssemblyNames(this Assembly ass)
    {
        if (ass.IsDynamic)
        {
            return Enumerable.Empty<AssemblyName>();
        }
        var fileNames = Directory.GetFiles(Path.GetDirectoryName(ass.Location), "*.dll");
        return fileNames.Select(item =>
        {
            if (TryGetAssemblyName(item, out AssemblyName assName))
            {
                assName.CodeBase = item;
                return assName;
            }
            return null;
        }).Where(item => item != null);
    }

    internal static bool TryGetAssemblyName(string path, out AssemblyName assemblyName)
    {
        try
        {
            assemblyName = AssemblyName.GetAssemblyName(path);
            return true;
        }
        catch (BadImageFormatException ex)
        {
            Console.WriteLine(ex.Message);
            assemblyName = null;
            return false;
        }
    }
    /// <summary>
    /// 获取自定义的程序集Name(通过publickeytoken是否为空判断)
    /// </summary>
    /// <param name="ass"></param>
    public static IEnumerable<AssemblyName> GetCustomerAssemblyNames(this Assembly ass)
    {
        return ass.GetAllAssemblyNames().Where(item => item.IsCustomerAssembly());
    }
    /// <summary>
    /// 获取所有 assembly
    /// </summary>
    public static IEnumerable<Assembly> GetAllAssemblies(this Assembly ass)
    {
        return ass.GetAllAssemblyNames().Select(item => Assembly.LoadFrom(item.CodeBase));
    }
    /// <summary>
    /// 获取所有自定义 assembly(通过publickeytoken是否为空判断)
    /// </summary>
    public static IEnumerable<Assembly> GetCustomerAssemblies(this Assembly ass)
    {
        return ass.GetCustomerAssemblyNames().Select(item => Assembly.LoadFrom(item.CodeBase));
    }

    /// <summary>
    /// 获取所有assembly
    /// </summary>
    /// <param name="domain"></param>
    /// <returns></returns>
    public static IEnumerable<Assembly> GetAllAssemblies(this AppDomain domain)
    {
        return domain.GetAssemblies().FirstOrDefault(item => item.IsCustomerAssembly()).GetAllAssemblies();
    }

    /// <summary>
    /// 获取自定义的程序集(通过publickeytoken是否为空判断)
    /// </summary>
    public static IEnumerable<Assembly> GetCustomerAssemblies(this AppDomain domain)
    {
        return domain.GetAssemblies().FirstOrDefault(item => item.IsCustomerAssembly()).GetCustomerAssemblies();
    }
    /// <summary>
    /// 获取所有接口
    /// </summary>
    public static IEnumerable<Type> GetInterfaces(this Assembly ass)
    {
        return ass.GetTypes().Where(item => item.IsInterface);
    }
    /// <summary>
    /// 获取所有 abstract class
    /// </summary>
    public static IEnumerable<Type> GetAbstracts(this Assembly ass)
    {
        return ass.GetTypes().Where(item => item.IsAbstract);
    }
    /// <summary>
    /// 获取所有枚举
    /// </summary>
    public static IEnumerable<Type> GetEnums(this Assembly ass)
    {
        return ass.GetTypes().Where(item => item.IsEnum);
    }
    /// <summary>
    /// 获取appdomain下所有type
    /// </summary>
    public static IEnumerable<Type> GetTypes(this AppDomain domain)
    {
        return domain.GetAssemblies().SelectMany(item => item.GetTypes());
    }
    /// <summary>
    /// 获取appdomain下所有自定义程序集的type
    /// </summary>
    public static IEnumerable<Type> GetCustomerTypes(this AppDomain domain)
    {
        return domain.GetCustomerAssemblies().SelectMany(item => item.GetTypes());
    }
    /// <summary>
    /// 获取appdomain下所有接口
    /// </summary>
    public static IEnumerable<Type> GetInterfaces(this AppDomain domain)
    {
        return domain.GetAssemblies().SelectMany(item => item.GetInterfaces());
    }
    /// <summary>
    /// 获取appdomain下所有自定义程序集的接口
    /// </summary>
    public static IEnumerable<Type> GetCustomerInterfaces(this AppDomain domain)
    {
        return domain.GetCustomerAssemblies().SelectMany(item => item.GetInterfaces());
    }
    public static IEnumerable<Type> GetTypes<T>(this AppDomain domain)
    {
        return domain.GetAllAssemblies().SelectMany(item => item.GetTypes()).Where(item => item.IsType<T>());
    }
    public static IEnumerable<Type> GetCustomerTypes<T>(this AppDomain domain)
    {
        return domain.GetCustomerTypes().Where(item => item.IsType<T>());
    }
    /// <summary>
    /// 根据前缀查找type
    /// </summary>
    /// <param name="domain"></param>
    /// <param name="prefixs"></param>
    public static IEnumerable<Type> GetCustomerTypesByPrefix(this AppDomain domain, params string[] prefixs)
    {
        return domain.GetCustomerTypes().Where(item => item.Name.HasStartsWith(prefixs));
    }
    /// <summary>
    /// 根据后缀查找type
    /// </summary>
    /// <param name="domain"></param>
    /// <param name="suffixs"></param>
    public static IEnumerable<Type> GetCustomerTypesBySuffix(this AppDomain domain, params string[] suffixs)
    {
        return domain.GetCustomerTypes().Where(item =>
        {
            return (item.IsGenericType ? item.FullName.Substring(0, item.FullName.IndexOf('`')) : item.Name).HasEndsWith(suffixs);
        });
    }
    /// <summary>
    /// 根据前后缀查找type
    /// </summary>
    /// <param name="domain"></param>
    /// <param name="prefixAndSuffixs"></param>
    public static IEnumerable<Type> GetCustomerTypesByPrefixAndSuffix(this AppDomain domain, params string[] prefixAndSuffixs)
    {
        return domain.GetCustomerTypes().Where(item =>
        {
            return item.Name.HasStartsWith(prefixAndSuffixs) || (item.IsGenericType ? item.FullName.Substring(0, item.FullName.IndexOf('`')) : item.Name).HasEndsWith(prefixAndSuffixs);
        });
    }
    public static IEnumerable<Type> GetTypes<T>(this Assembly ass)
    {
        return ass.GetAllAssemblies().SelectMany(item => item.GetTypes()).Where(item => item.IsType<T>());
    }
    /// <summary>
    /// 根据前缀查找type
    /// </summary>
    /// <param name="ass"></param>
    /// <param name="prefixs"></param>
    public static IEnumerable<Type> GetByPrefix(this Assembly ass, params string[] prefixs)
    {
        return ass.GetTypes().Where(item => item.Name.HasStartsWith(prefixs));
    }
    /// <summary>
    /// 根据后缀查找type
    /// </summary>
    /// <param name="ass"></param>
    /// <param name="suffixs"></param>
    public static IEnumerable<Type> GetBySuffix(this Assembly ass, params string[] suffixs)
    {
        return ass.GetTypes().Where(item =>
        {
            return (item.IsGenericType ? item.FullName.Substring(0, item.FullName.IndexOf('`')) : item.FullName).HasEndsWith(suffixs);
        });
    }
    /// <summary>
    /// 根据前后缀查找type
    /// </summary>
    /// <param name="ass"></param>
    /// <param name="prefixAndSuffixs"></param>
    public static IEnumerable<Type> GetByPrefixAndSuffix(this Assembly ass, params string[] prefixAndSuffixs)
    {
        return ass.GetTypes().Where(item =>
        {
            return item.Name.HasStartsWith(prefixAndSuffixs) || (item.IsGenericType ? item.FullName.Substring(0, item.FullName.IndexOf('`')) : item.Name).HasEndsWith(prefixAndSuffixs);
        });
    }
}