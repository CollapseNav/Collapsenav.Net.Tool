using System.Reflection;

namespace Collapsenav.Net.Tool;

public static partial class AssemblyExt
{
    /// <summary>
    /// 获取自定义的程序集Name(通过publickeytoken是否为空判断)
    /// </summary>
    /// <param name="ass"></param>
    /// <param name="scanAll">是否扫描全部程序集(包括非自定义的)默认为false</param>
    public static IEnumerable<AssemblyName> GetCustomerAssemblies(this Assembly ass, bool scanAll = false)
    {
        return ass.GetReferencedAssemblies().Where(item => scanAll || item.GetPublicKeyToken().IsEmpty());
    }

    /// <summary>
    /// 获取自定义的程序集(通过publickeytoken是否为空判断)
    /// </summary>
    public static IEnumerable<Assembly> GetCustomerAssemblies(this AppDomain domain)
    {
        return domain.GetAssemblies().Where(item => item.GetName().GetPublicKeyToken().IsEmpty());
    }
}