using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Collapsenav.Net.Tool.WebApi.Test;
[TestCaseOrderer("Collapsenav.Net.Tool.WebApi.Test.TestOrders", "Collapsenav.Net.Tool.WebApi.Test")]
public class NotBaseModifyRepControllerTest
{
    protected readonly IServiceProvider Provider;
    public NotBaseModifyRepControllerTest()
    {
        Provider = DIConfig.GetNotBaseProvider();
    }
    protected T GetService<T>()
    {
        return Provider.GetService<T>();
    }
    [Fact, Order(1)]
    public async Task AddTest()
    {
        using var RepController = GetService<IModifyController<TestNotBaseModifyEntity, TestNotBaseModifyEntityCreate>>();
        var entitys = new List<TestNotBaseModifyEntityCreate>{
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
        var queryController = GetService<IQueryController<TestNotBaseModifyEntity, TestNotBaseModifyEntityGet>>();
        var data = await queryController.QueryAsync(new TestNotBaseModifyEntityGet { Code = "wait-to-delete" });
        Assert.True(data.Count() == 10);
    }

    [Fact, Order(4)]
    public async Task DeleteTest()
    {
        var queryController = GetService<IQueryController<TestNotBaseModifyEntity, TestNotBaseModifyEntityGet>>();
        using var RepController = GetService<IModifyController<TestNotBaseModifyEntity, TestNotBaseModifyEntityCreate>>();
        var data = await queryController.QueryAsync(new TestNotBaseModifyEntityGet { Code = "wait-to-delete" });
        await RepController.DeleteAsync(data.First().Id.ToString(), true);
        data.ForEach(async (item) =>
        {
            await RepController.DeleteAsync(item.Id.ToString(), true);
        });
        RepController.Dispose();
        queryController = GetService<IQueryController<TestNotBaseModifyEntity, TestNotBaseModifyEntityGet>>();
        data = await queryController.QueryAsync(new TestNotBaseModifyEntityGet { Code = "wait-to-delete" });
        Assert.True(data.IsEmpty());
    }
}
