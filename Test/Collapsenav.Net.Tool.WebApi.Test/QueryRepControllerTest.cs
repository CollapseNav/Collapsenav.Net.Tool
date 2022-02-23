using System;
using System.Linq;
using System.Threading.Tasks;
using Collapsenav.Net.Tool.Data;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Collapsenav.Net.Tool.WebApi.Test;
public class QueryControllerTest
{
    protected readonly IServiceProvider Provider;
    protected readonly IQueryRepController<int, TestQueryEntity, TestQueryEntityGet> RepController;
    public QueryControllerTest()
    {
        Provider = DIConfig.GetProvider();
        RepController = GetService<IQueryRepController<int, TestQueryEntity, TestQueryEntityGet>>();
    }
    protected T GetService<T>()
    {
        return Provider.GetService<T>();
    }

    [Fact]
    public async Task QueryTest()
    {
        var data = await RepController.QueryAsync(new TestQueryEntityGet { });
        Assert.True(data.Count() == 10);
        data = await RepController.QueryAsync(new TestQueryEntityGet { Id = 2 });
        Assert.True(data.Count() == 1);
        data = await RepController.QueryAsync(new TestQueryEntityGet { Number = 233 });
        Assert.True(data.Count() == 8);
        data = await RepController.QueryAsync(new TestQueryEntityGet { Code = "Bxe" });
        Assert.True(data.Count() == 1);
    }

    [Fact]
    public async Task PageTest()
    {
        var data = await RepController.QueryPageAsync(new TestQueryEntityGet { });
        Assert.True(data.Length == 10);
        data = await RepController.QueryPageAsync(new TestQueryEntityGet { Id = 2 });
        Assert.True(data.Length == 1);
        data = await RepController.QueryPageAsync(new TestQueryEntityGet { Number = 233 });
        Assert.True(data.Length == 8);
        data = await RepController.QueryPageAsync(new TestQueryEntityGet { Code = "2333" });
        Assert.True(data.Length == 0);
        data = await RepController.QueryPageAsync(new TestQueryEntityGet { IsTest = false });
        Assert.True(data.Length == 0);
        data = await RepController.QueryPageAsync(new TestQueryEntityGet { Number = 233, Code = "Bxe" });
        Assert.True(data.Length == 1);
        data = await RepController.QueryPageAsync(new TestQueryEntityGet { Number = 2333, Code = "Bxe" });
        Assert.True(data.Length == 0);
        data = await RepController.QueryPageAsync(new TestQueryEntityGet { Number = 233 }, new PageRequest { Max = 3 });
        Assert.True(data.Length == 3);
        Assert.True(data.Total == 8);
        data = await RepController.QueryPageAsync(new TestQueryEntityGet { Number = 233 }, new PageRequest { Index = 2, Max = 3 });
        Assert.True(data.Length == 3);
        Assert.True(data.Total == 8);
        data = await RepController.QueryPageAsync(new TestQueryEntityGet { Number = 233  }, new PageRequest { Index = 3, Max = 3 });
        Assert.True(data.Length == 2);
        Assert.True(data.Total == 8);
    }

    [Fact]
    public async Task FindByIdTest()
    {
        var data = await RepController.QueryAsync(5);
        Assert.True(data.Number == 520);
        var datas = await RepController.QueryByIdsAsync(new[] { 7, 8, 9, 10 });
        Assert.True(datas.Count() == 4);
        datas = await RepController.QueryByIdsPostAsync(new[] { 3, 6, 7, 11 });
        Assert.True(datas.Count() == 3);
    }
}


