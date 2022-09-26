using System;
using System.Linq;
using System.Threading.Tasks;
using Collapsenav.Net.Tool.Data;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Collapsenav.Net.Tool.WebApi.Test;
public class NotBaseQueryAppControllerTest
{
    protected readonly IServiceProvider Provider;
    protected readonly IQueryController<TestNotBaseQueryEntity, TestNotBaseQueryEntityGet> RepController;
    public NotBaseQueryAppControllerTest()
    {
        Provider = DIConfig.GetNotBaseAppProvider();
        RepController = GetService<IQueryController<TestNotBaseQueryEntity, TestNotBaseQueryEntityGet>>();
    }
    protected T GetService<T>()
    {
        return Provider.GetService<T>();
    }

    [Fact]
    public async Task QueryTest()
    {
        var data = await RepController.QueryAsync(new TestNotBaseQueryEntityGet { });
        Assert.True(data.Count() == 10);
        data = await RepController.QueryAsync(new TestNotBaseQueryEntityGet { Id = 2 });
        Assert.True(data.Count() == 1);
        data = await RepController.QueryAsync(new TestNotBaseQueryEntityGet { Number = 233 });
        Assert.True(data.Count() == 8);
        data = await RepController.QueryAsync(new TestNotBaseQueryEntityGet { Code = "Bxe" });
        Assert.True(data.Count() == 1);
    }

    [Fact]
    public async Task PageTest()
    {
        var data = await RepController.QueryPageAsync(new TestNotBaseQueryEntityGet { });
        Assert.True(data.Length == 10);
        data = await RepController.QueryPageAsync(new TestNotBaseQueryEntityGet { Id = 2 });
        Assert.True(data.Length == 1);
        data = await RepController.QueryPageAsync(new TestNotBaseQueryEntityGet { Number = 233 });
        Assert.True(data.Length == 8);
        data = await RepController.QueryPageAsync(new TestNotBaseQueryEntityGet { Code = "2333" });
        Assert.True(data.Length == 0);
        data = await RepController.QueryPageAsync(new TestNotBaseQueryEntityGet { IsTest = false });
        Assert.True(data.Length == 0);
        data = await RepController.QueryPageAsync(new TestNotBaseQueryEntityGet { Number = 233, Code = "Bxe" });
        Assert.True(data.Length == 1);
        data = await RepController.QueryPageAsync(new TestNotBaseQueryEntityGet { Number = 2333, Code = "Bxe" });
        Assert.True(data.Length == 0);
        data = await RepController.QueryPageAsync(new TestNotBaseQueryEntityGet { Number = 233 }, new PageRequest { Max = 3 });
        Assert.True(data.Length == 3);
        Assert.True(data.Total == 8);
        data = await RepController.QueryPageAsync(new TestNotBaseQueryEntityGet { Number = 233 }, new PageRequest { Index = 2, Max = 3 });
        Assert.True(data.Length == 3);
        Assert.True(data.Total == 8);
        data = await RepController.QueryPageAsync(new TestNotBaseQueryEntityGet { Number = 233 }, new PageRequest { Index = 3, Max = 3 });
        Assert.True(data.Length == 2);
        Assert.True(data.Total == 8);
    }
    [Fact]
    public async Task FindByIdTest()
    {
        var data = await RepController.QueryAsync("5");
        Assert.True(data.Number == 520);
    }
}
