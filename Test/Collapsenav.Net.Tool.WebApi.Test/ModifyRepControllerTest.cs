using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Collapsenav.Net.Tool.Data;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Collapsenav.Net.Tool.WebApi.Test;
[TestCaseOrderer("Collapsenav.Net.Tool.WebApi.Test.TestOrders", "Collapsenav.Net.Tool.WebApi.Test")]
public class ModifyRepControllerTest
{
    protected readonly IServiceProvider Provider;
    public ModifyRepControllerTest()
    {
        Provider = DIConfig.GetProvider();
    }
    protected T GetService<T>()
    {
        return Provider.GetService<T>();
    }
    [Fact, Order(1)]
    public async Task AddTest()
    {
        using var RepController = GetService<IModifyRepController<int, TestModifyEntity, TestModifyEntityCreate, TestModifyEntityGet>>();
        var entitys = new List<TestModifyEntityCreate>{
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
    }

    [Fact, Order(2)]
    public async Task CheckAddTest()
    {
        using var RepController = GetService<IModifyRepController<int, TestModifyEntity, TestModifyEntityCreate, TestModifyEntityGet>>();
        var data = await RepController.FindQueryAsync(new() { Code = "wait-to-delete" });
        Assert.True(data.Count() == 10);
        Assert.True(data.First().Number == 23334);
        Assert.True(data.Skip(1).All(item => item.Number == 23333));
    }
    [Fact, Order(3)]
    public async Task UpdateTest()
    {
        using var RepController = GetService<IModifyRepController<int, TestModifyEntity, TestModifyEntityCreate, TestModifyEntityGet>>();
        var data = await RepController.FindQueryAsync(new() { Code = "wait-to-delete" });
        foreach (var item in data)
            await RepController.UpdateAsync(item.Id, new(item.Code, item.Number + 1, !item.IsTest));
    }
    [Fact, Order(4)]
    public async Task CheckUpdateTest()
    {
        using var RepController = GetService<IModifyRepController<int, TestModifyEntity, TestModifyEntityCreate, TestModifyEntityGet>>();
        var data = await RepController.FindQueryAsync(new() { Code = "wait-to-delete" });
        Assert.True(data.All(item => !item.IsTest.Value));
        Assert.True(data.First().Number == 23335);
        Assert.True(data.Skip(1).All(item => item.Number == 23334));
    }
    [Fact, Order(4)]
    public async Task DeleteTest()
    {
        using var RepController = GetService<IModifyRepController<int, TestModifyEntity, TestModifyEntityCreate, TestModifyEntityGet>>();
        var data = await RepController.FindQueryAsync(new() { Code = "wait-to-delete" });
        await RepController.DeleteAsync(data.First().Id, true);
    }
}
