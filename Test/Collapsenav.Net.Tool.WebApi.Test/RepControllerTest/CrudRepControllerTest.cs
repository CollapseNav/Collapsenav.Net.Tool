using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Collapsenav.Net.Tool.WebApi.Test;
[TestCaseOrderer("Collapsenav.Net.Tool.WebApi.Test.TestOrders", "Collapsenav.Net.Tool.WebApi.Test")]
public class CrudRepControllerTest
{
    protected readonly IServiceProvider Provider;
    public CrudRepControllerTest()
    {
        Provider = DIConfig.GetProvider();
    }
    protected T GetService<T>()
    {
        return Provider.GetService<T>();
    }

    [Fact, Order(1)]
    public async Task QueryTest()
    {
        using var RepController = GetService<ICrudController<int, TestEntity, TestEntityCreate, TestEntityGet>>();
        var data = await RepController.QueryAsync(new TestEntityGet { });
        Assert.True(data.Count() == 10);
        data = await RepController.QueryAsync(new TestEntityGet { Id = 2 });
        Assert.True(data.Count() == 1);
        data = await RepController.QueryAsync(new TestEntityGet { Number = 233 });
        Assert.True(data.Count() == 7);
        data = await RepController.QueryAsync(new TestEntityGet { Code = "Euw" });
        Assert.True(data.Count() == 1);
    }

    [Fact, Order(2)]
    public async Task PageTest()
    {
        using var RepController = GetService<ICrudController<int, TestEntity, TestEntityCreate, TestEntityGet>>();
        var data = await RepController.QueryPageAsync(new TestEntityGet { });
        Assert.True(data.Length == 10);
        data = await RepController.QueryPageAsync(new TestEntityGet { Id = 2 });
        Assert.True(data.Length == 1);
        data = await RepController.QueryPageAsync(new TestEntityGet { Number = 233 });
        Assert.True(data.Length == 7);
        data = await RepController.QueryPageAsync(new TestEntityGet { Code = "2333" });
        Assert.True(data.Length == 0);
        data = await RepController.QueryPageAsync(new TestEntityGet { IsTest = false });
        Assert.True(data.Length == 0);
        data = await RepController.QueryPageAsync(new TestEntityGet { Code = "Euw" });
        Assert.True(data.Length == 1);
        data = await RepController.QueryPageAsync(new TestEntityGet { Number = 2333, Code = "Euw" });
        Assert.True(data.Length == 0);
        data = await RepController.QueryPageAsync(new TestEntityGet { Number = 233 }, new PageRequest { Max = 3 });
        Assert.True(data.Length == 3);
        Assert.True(data.Total == 7);
        data = await RepController.QueryPageAsync(new TestEntityGet { Number = 233 }, new PageRequest { Index = 2, Max = 3 });
        Assert.True(data.Length == 3);
        Assert.True(data.Total == 7);
        data = await RepController.QueryPageAsync(new TestEntityGet { Number = 233 }, new PageRequest { Index = 3, Max = 3 });
        Assert.True(data.Length == 1);
        Assert.True(data.Total == 7);
    }

    [Fact, Order(3)]
    public async Task FindByIdTest()
    {
        using var RepController = GetService<ICrudController<int, TestEntity, TestEntityCreate, TestEntityGet>>();
        var data = await RepController.QueryAsync(7);
        Assert.True(data.Number == 850);
        var datas = await RepController.QueryByIdsAsync(new[] { 7, 8, 9, 10 });
        Assert.True(datas.Count() == 4);
        datas = await RepController.QueryByIdsPostAsync(new[] { 3, 6, 7, 11 });
        Assert.True(datas.Count() == 3);
    }
    [Fact, Order(5)]
    public async Task AddTest()
    {
        using var RepController = GetService<ICrudController<int, TestEntity, TestEntityCreate, TestEntityGet>>();
        var entitys = new List<TestEntityCreate>{
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
        using var queryController = GetService<ICrudController<int, TestEntity, TestEntityCreate, TestEntityGet>>();
        var data = await queryController.QueryAsync(new TestEntityGet { Code = "wait-to-delete" });
        Assert.True(data.Count() == 10);
    }

    [Fact, Order(6)]
    public async Task UpdateTest()
    {
        var queryController = GetService<ICrudController<int, TestEntity, TestEntityCreate, TestEntityGet>>();
        using var RepController = GetService<ICrudController<int, TestEntity, TestEntityCreate, TestEntityGet>>();
        var data = await queryController.QueryAsync(new TestEntityGet { Code = "wait-to-delete", Number = 23333 });
        foreach (var item in data)
            await RepController.UpdateAsync(item.Id, new(item.Code, item.Number + 1, !item.IsTest));
        RepController.Dispose();
        queryController = GetService<ICrudController<int, TestEntity, TestEntityCreate, TestEntityGet>>();
        var pageData = await queryController.QueryPageAsync(new TestEntityGet { Code = "wait-to-delete", Number = 23333 });
        Assert.True(pageData.Length == 1);
        Assert.True(pageData.Data.All(item => item.Number == 23335));
    }
    [Fact, Order(7)]
    public async Task DeleteTest()
    {
        var queryController = GetService<ICrudController<int, TestEntity, TestEntityCreate, TestEntityGet>>();
        using var RepController = GetService<ICrudController<int, TestEntity, TestEntityCreate, TestEntityGet>>();
        var data = await queryController.QueryAsync(new TestEntityGet { Code = "wait-to-delete" });
        await RepController.DeleteAsync(data.First().Id, true);
        await RepController.DeleteRangeAsync(data.Skip(1).Select(item => item.Id), true);
        RepController.Dispose();
        queryController = GetService<ICrudController<int, TestEntity, TestEntityCreate, TestEntityGet>>();
        data = await queryController.QueryAsync(new TestEntityGet { Code = "wait-to-delete" });
        Assert.True(data.IsEmpty());
    }
}
