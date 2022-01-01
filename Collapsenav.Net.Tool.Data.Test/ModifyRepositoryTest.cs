using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Collapsenav.Net.Tool.Data.Test;
[TestCaseOrderer("Collapsenav.Net.Tool.Data.Test.TestOrders", "Collapsenav.Net.Tool.Data.Test")]
public class ModifyRepositoryTest
{
    protected readonly IServiceProvider Provider;
    protected readonly IModifyRepository<TestModifyEntity> Repository;
    public ModifyRepositoryTest()
    {
        Provider = DIConfig.GetProvider();
        Repository = GetService<IModifyRepository<TestModifyEntity>>();
    }
    protected T GetService<T>()
    {
        return Provider.GetService<T>();
    }

    [Fact, Order(21)]
    public async Task ModifyRepositoryAddTest()
    {
        var entitys = new List<TestModifyEntity>{
                new (11,"23333",2333,true),
                new (12,"23333",2333,true),
                new (13,"23333",2333,true),
                new (14,"23333",2333,true),
                new (15,"23333",2333,true),
                new (16,"23333",2333,true),
                new (17,"23333",2333,true),
                new (18,"23333",2333,true),
                new (19,"23333",2333,true),
                new (20,"23333",2333,true),
            };
        await Repository.AddAsync(entitys);
        await Repository.SaveAsync();
        var data = await Repository.FindAsync(item => true);
        Assert.True(data.Count() == 10);
    }

    [Fact, Order(22)]
    public async Task ModifyRepositoryUpdateTest()
    {
        var updateCount = await Repository.UpdateAsync(item => item.Id > 18, entity => new TestModifyEntity { Number = 123 });
        await Repository.SaveAsync();
        var numberEqual123 = await Repository.FindAsync(item => item.Number == 123);
        Assert.True(updateCount == 2);
        Assert.True(numberEqual123.Count() == 2);
    }

    [Fact, Order(23)]
    public async Task ModifyRepositoryDeleteTest()
    {
        var delCount = await Repository.DeleteAsync(item => item.Id < 13, true);
        await Repository.SaveAsync();
        var leftData = await Repository.FindAsync(item => true);
        Assert.True(delCount == 2);
        Assert.True(leftData.Count() == 8);
    }
    [Fact, Order(24)]
    public async Task ModifyRepositoryDeleteAllTest()
    {
        var delCount = await Repository.DeleteAsync(item => true, true);
        await Repository.SaveAsync();
        var leftData = await Repository.FindAsync(item => true);
        Assert.True(delCount == 8);
        Assert.True(leftData.IsEmpty());
    }
}
