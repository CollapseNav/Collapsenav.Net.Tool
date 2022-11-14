using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Collapsenav.Net.Tool.WebApi.Test;

public class DynamicApiTest
{
    public DynamicApiTest()
    {
        (Collection, Provider) = DIConfig.GetDynamicApiProvider();
    }
    protected readonly IServiceProvider Provider;
    protected readonly IServiceCollection Collection;

    [Fact]
    public void ApiTest()
    {
        Assert.Contains(Collection, item => item?.ImplementationType?.Name == "DTestQueryController");
        Assert.Contains(Collection, item => item?.ImplementationType?.Name == "DTestIntQueryController");
        Assert.Contains(Collection, item => item?.ImplementationType?.Name == "DTestModifyController");
        Assert.Contains(Collection, item => item?.ImplementationType?.Name == "DTestIntModifyController");
        Assert.Contains(Collection, item => item?.ImplementationType?.Name == "DTestCrudController");
        Assert.Contains(Collection, item => item?.ImplementationType?.Name == "DTestIntCrudController");
    }
}