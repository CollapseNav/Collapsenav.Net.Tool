using System.Collections;
using System.Reflection;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Collapsenav.Net.Tool.Test;

public class AssemblyTest
{
    [Fact]
    public void GetCustomerAssemblyNameTest()
    {
        var allAssNames = Assembly.GetEntryAssembly().GetAllAssemblyNames();
        var customerAssNames = Assembly.GetEntryAssembly().GetCustomerAssemblyNames();
        Assert.Contains(allAssNames, item => item.Name.HasStartsWith(new[] { "System", "Microsoft" }, true));
        Assert.Contains(customerAssNames, item => !item.Name.HasStartsWith(new[] { "System", "Microsoft" }, true));
    }

    [Fact]
    public void GetCustomerAssembliesTest()
    {
        var allAsses = Assembly.GetEntryAssembly().GetAllAssemblies().ToList();
        var customerAsses = Assembly.GetEntryAssembly().GetCustomerAssemblies().ToList();
        Assert.Contains(allAsses, item => item.FullName.HasStartsWith(new[] { "System", "Microsoft" }, true));
        Assert.Contains(customerAsses, item => !item.FullName.HasStartsWith(new[] { "System", "Microsoft" }, true));
    }

    [Fact]
    public void GetAllAssembliesByDomainTest()
    {
        var allAsses = AppDomain.CurrentDomain.GetAllAssemblies();
        var customerAsses = AppDomain.CurrentDomain.GetCustomerAssemblies();

        Assert.Contains(allAsses, item => item.FullName.HasStartsWith(new[] { "System", "Microsoft" }, true));
        Assert.Contains(customerAsses, item => !item.FullName.HasStartsWith(new[] { "System", "Microsoft" }, true));
    }

    [Fact]
    public void GetInterfacesTest()
    {
        var interfaces = Assembly.GetExecutingAssembly().GetInterfaces().ToList();
        Assert.True(interfaces.All(type => type.IsInterface));
        Assert.True(interfaces.AllContain(typeof(ITestInterface), typeof(ITestInterface<>), typeof(ITestInterface<,>)));

        interfaces = AppDomain.CurrentDomain.GetInterfaces().ToList();
        Assert.True(interfaces.All(type => type.IsInterface));
        Assert.True(interfaces.AllContain(typeof(ITestInterface), typeof(ITestInterface<>), typeof(ITestInterface<,>)));
        Assert.Contains(interfaces, type => type.FullName.HasStartsWith("System", "Microsoft"));

        interfaces = AppDomain.CurrentDomain.GetCustomerInterfaces().ToList();
        Assert.True(interfaces.All(type => type.IsInterface));
        Assert.True(interfaces.AllContain(typeof(ITestInterface), typeof(ITestInterface<>), typeof(ITestInterface<,>)));
        Assert.Contains(interfaces, type => !type.FullName.HasStartsWith("System", "Microsoft"));
    }

    [Fact]
    public void GetAbstractClassTest()
    {
        var absClasses = Assembly.GetExecutingAssembly().GetAbstracts().ToList();
        Assert.True(absClasses.All(type => type.IsAbstract));
        Assert.True(absClasses.AllContain(typeof(AbsClass)));
    }

    [Fact]
    public void GetEnumsTest()
    {
        var enums = Assembly.GetExecutingAssembly().GetEnums().ToList();
        Assert.True(enums.All(type => type.IsEnum));
        Assert.True(enums.AllContain(typeof(TestEnum)));
    }

    [Fact]
    public void GetTypesTest()
    {
        var types = AppDomain.CurrentDomain.GetTypes<ITestInterface>();
        Assert.True(types.AllContain(typeof(ITestInterface<>), typeof(ITestInterface<,>), typeof(InterfaceTestClass), typeof(InterfaceTestClass2), typeof(InterfaceTestClass3)));

        types = AppDomain.CurrentDomain.GetCustomerTypes<ITestInterface>();
        Assert.True(types.AllContain(typeof(ITestInterface<>), typeof(ITestInterface<,>), typeof(InterfaceTestClass), typeof(InterfaceTestClass2), typeof(InterfaceTestClass3)));

        types = AppDomain.CurrentDomain.GetTypes<IEnumerable>();
        Assert.True(types.AllContain(typeof(JEnumerable<>)));

        types = AppDomain.CurrentDomain.GetCustomerTypes<IEnumerable>();
        Assert.False(types.AllContain(typeof(JEnumerable<>)));
        Assert.True(types.AllContain(typeof(MyEnumerable)));
    }

    [Fact]
    public void GetCustomerTypesByPrefixTest()
    {
        var types = AppDomain.CurrentDomain.GetCustomerTypesByPrefix("Interface");
        Assert.True(types.AllContain(typeof(InterfaceTestClass), typeof(InterfaceTestClass2), typeof(InterfaceTestClass3)));
    }

    [Fact]
    public void GetCustomerTypesBySuffixTest()
    {
        var types = AppDomain.CurrentDomain.GetCustomerTypesBySuffix("Interface");
        Assert.True(types.AllContain(typeof(ITestInterface), typeof(ITestInterface<>), typeof(ITestInterface<,>)));
    }

    [Fact]
    public void GetCustomerTypesByPrefixAndSuffixTest()
    {
        var types = AppDomain.CurrentDomain.GetCustomerTypesByPrefixAndSuffix("Interface");
        Assert.True(types.AllContain(typeof(InterfaceTestClass), typeof(InterfaceTestClass2), typeof(InterfaceTestClass3), typeof(ITestInterface), typeof(ITestInterface<>), typeof(ITestInterface<,>)));
    }
}

