using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Collapsenav.Net.Tool.Data;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Collapsenav.Net.Tool.WebApi.Test;
[TestCaseOrderer("Collapsenav.Net.Tool.WebApi.Test.TestOrders", "Collapsenav.Net.Tool.WebApi.Test")]
public class NotBaseCrudAppControllerTest
{
    protected readonly IServiceProvider Provider;
    public NotBaseCrudAppControllerTest()
    {
        Provider = DIConfig.GetNotBaseAppProvider();
    }
    protected T GetService<T>()
    {
        return Provider.GetService<T>();
    }

    [Fact, Order(1)]
    public async Task QueryTest()
    {
        using var RepController = GetService<ICrudController<TestNotBaseEntity, TestNotBaseEntityCreate, TestNotBaseEntityGet>>();
        var data = await RepController.QueryAsync(new TestNotBaseEntityGet { });
        Assert.True(data.Count() == 10);
        data = await RepController.QueryAsync(new TestNotBaseEntityGet { Id = 2 });
        Assert.True(data.Count() == 1);
        data = await RepController.QueryAsync(new TestNotBaseEntityGet { Number = 233 });
        Assert.True(data.Count() == 7);
        data = await RepController.QueryAsync(new TestNotBaseEntityGet { Code = "Euw" });
        Assert.True(data.Count() == 1);
    }

    [Fact, Order(2)]
    public async Task PageTest()
    {
        using var RepController = GetService<ICrudController<TestNotBaseEntity, TestNotBaseEntityCreate, TestNotBaseEntityGet>>();
        var data = await RepController.QueryPageAsync(new TestNotBaseEntityGet { });
        Assert.True(data.Length == 10);
        data = await RepController.QueryPageAsync(new TestNotBaseEntityGet { Id = 2 });
        Assert.True(data.Length == 1);
        data = await RepController.QueryPageAsync(new TestNotBaseEntityGet { Number = 233 });
        Assert.True(data.Length == 7);
        data = await RepController.QueryPageAsync(new TestNotBaseEntityGet { Code = "2333" });
        Assert.True(data.Length == 0);
        data = await RepController.QueryPageAsync(new TestNotBaseEntityGet { IsTest = false });
        Assert.True(data.Length == 0);
        data = await RepController.QueryPageAsync(new TestNotBaseEntityGet { Code = "Euw" });
        Assert.True(data.Length == 1);
        data = await RepController.QueryPageAsync(new TestNotBaseEntityGet { Number = 2333, Code = "Euw" });
        Assert.True(data.Length == 0);
        data = await RepController.QueryPageAsync(new TestNotBaseEntityGet { Number = 233 }, new PageRequest { Max = 3 });
        Assert.True(data.Length == 3);
        Assert.True(data.Total == 7);
        data = await RepController.QueryPageAsync(new TestNotBaseEntityGet { Number = 233 }, new PageRequest { Index = 2, Max = 3 });
        Assert.True(data.Length == 3);
        Assert.True(data.Total == 7);
        data = await RepController.QueryPageAsync(new TestNotBaseEntityGet { Number = 233 }, new PageRequest { Index = 3, Max = 3 });
        Assert.True(data.Length == 1);
        Assert.True(data.Total == 7);
    }

    [Fact, Order(3)]
    public async Task FindByIdTest()
    {
        using var RepController = GetService<ICrudController<TestNotBaseEntity, TestNotBaseEntityCreate, TestNotBaseEntityGet>>();
        var data = await RepController.QueryAsync("7");
        Assert.True(data.Number == 850);
    }

    [Fact, Order(5)]
    public async Task AddTest()
    {
        using var RepController = GetService<ICrudController<TestNotBaseEntity, TestNotBaseEntityCreate, TestNotBaseEntityGet>>();
        var entitys = new List<TestNotBaseEntityCreate>{
                new ("wait-to-delete",23334,true),
                new ("wait-to-delete",23333,true),
                new ("wait-to-delete",23333,true),
                new ("wait-to-delete",23333,true),
                new ("wait-to-delete",23333,true),
                new ("wait-to-delete",23333,true),
                new ("wait-to-delete",23333,true),
                new ("wait-to-delete",23333,true),
                new ("wait-to-delete",23333,true),
                new ("wait-to-delete",23333,true),
            };
        await RepController.AddAsync(entitys.First());
        await RepController.AddRangeAsync(entitys.Skip(1));
        RepController.Dispose();
        using var queryController = GetService<ICrudController<TestNotBaseEntity, TestNotBaseEntityCreate, TestNotBaseEntityGet>>();
        var data = await queryController.QueryAsync(new TestNotBaseEntityGet { Code = "wait-to-delete" });
        Assert.True(data.Count() == 10);
    }

    [Fact, Order(7)]
    public async Task DeleteTest()
    {
        var queryController = GetService<ICrudController<TestNotBaseEntity, TestNotBaseEntityCreate, TestNotBaseEntityGet>>();
        using var RepController = GetService<ICrudController<TestNotBaseEntity, TestNotBaseEntityCreate, TestNotBaseEntityGet>>();
        var data = await queryController.QueryAsync(new TestNotBaseEntityGet { Code = "wait-to-delete" });
        await RepController.DeleteAsync(data.First().Id.ToString(), true);
        data.ForEach(async (item) =>
        {
            await RepController.DeleteAsync(item.Id.ToString(), true);
        });
        RepController.Dispose();
        queryController = GetService<ICrudController<TestNotBaseEntity, TestNotBaseEntityCreate, TestNotBaseEntityGet>>();
        data = await queryController.QueryAsync(new TestNotBaseEntityGet { Code = "wait-to-delete" });
        Assert.True(data.IsEmpty());
    }
}
