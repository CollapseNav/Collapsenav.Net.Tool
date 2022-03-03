using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Collapsenav.Net.Tool.Data.Test;
[TestCaseOrderer("Collapsenav.Net.Tool.Data.Test.TestOrders", "Collapsenav.Net.Tool.Data.Test")]
public class NotBaseModifyRepositoryTest
{
    protected readonly IServiceProvider Provider;
    protected readonly IModifyRepository<TestNotBaseModifyEntity> Repository;
    protected readonly IQueryRepository<TestNotBaseModifyEntity> Read;
    public NotBaseModifyRepositoryTest()
    {
        Provider = DIConfig.GetNotBaseProvider();
        Repository = GetService<IModifyRepository<TestNotBaseModifyEntity>>();
        Read = GetService<IQueryRepository<TestNotBaseModifyEntity>>();
    }
    protected T GetService<T>()
    {
        return Provider.GetService<T>();
    }

    [Fact, Order(51)]
    public async Task ModifyRepositoryAddTest()
    {
        var entitys = new List<TestNotBaseModifyEntity>{
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
        await Repository.AddAsync(entitys.First());
        await Repository.AddAsync(entitys.Skip(1));
        await Repository.SaveAsync();
        var data = await Read.QueryAsync(item => true);
        Console.WriteLine(data.Count());
        data.Count().ToString().ToBytes().SaveTo("./count");
        Assert.True(data.Count() == 20);
    }

    [Fact, Order(52)]
    public async Task ModifyRepositoryUpdateTest()
    {
        var updateCount = await Repository.UpdateAsync(item => item.Id > 18, entity => new TestNotBaseModifyEntity { Number = 123 });
        await Repository.SaveAsync();
        var numberEqual123 = await Read.QueryAsync(item => item.Number == 123);
        Assert.True(updateCount == 2);
        Assert.True(numberEqual123.Count() == 2);
    }

    [Fact, Order(53)]
    public async Task ModifyRepositorySoftDeleteTest()
    {
        var delCount = await Repository.DeleteAsync(item => item.Id < 11, false);
        await Repository.SaveAsync();
        Assert.True(delCount == 0);
        await Repository.DeleteAsync(11, false);
        Repository.Save();
        var leftData = await Read.QueryAsync();
        Assert.True(leftData.Count() == 20);
    }

    [Fact, Order(54)]
    public async Task ModifyRepositoryDeleteTest()
    {
        var delCount = await Repository.DeleteAsync(item => item.Id < 11, true);
        await Repository.SaveAsync();
        Assert.True(delCount == 10);
        await Repository.DeleteAsync(11, true);
        await Repository.DeleteAsync(12, true);
        Repository.Save();
        var leftData = await Read.QueryAsync(item => true);
        Assert.True(leftData.Count() == 8);
    }


    [Fact, Order(55)]
    public async Task ModifyRepositoryDeleteAllTest()
    {
        var delCount = await Repository.DeleteAsync(item => true, true);
        await Repository.SaveAsync();
        var leftData = await Read.QueryAsync(item => true);
        Assert.True(delCount == 8);
        Assert.True(leftData.IsEmpty());
    }
}
