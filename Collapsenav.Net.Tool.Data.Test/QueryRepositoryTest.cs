using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Collapsenav.Net.Tool.Data.Test;
[TestCaseOrderer("Collapsenav.Net.Tool.Data.Test.TestOrders", "Collapsenav.Net.Tool.Data.Test")]
public class QueryRepositoryTest
{
    protected readonly IServiceProvider Provider;
    protected readonly IQueryRepository<TestQueryEntity> Repository;
    protected readonly IQueryRepository<TestQueryEntity> TestRepository;
    public QueryRepositoryTest()
    {
        Provider = DIConfig.GetProvider();
        Repository = GetService<IQueryRepository<TestQueryEntity>>();
        TestRepository = GetService<IQueryRepository<TestQueryEntity>>();
    }
    protected T GetService<T>()
    {
        return Provider.GetService<T>();
    }

    [Fact, Order(11)]
    public async Task QueryRepositoryAddTest()
    {
        var entitys = new List<TestQueryEntity>{
                new (1,"2333",2333,true),
                new (2,"2333",2333,true),
                new (3,"2333",2333,true),
                new (4,"2333",2333,true),
                new (5,"2333",2333,true),
                new (6,"2333",2333,true),
                new (7,"2333",2333,true),
                new (8,"2333",2333,true),
                new (9,"2333",2333,true),
                new (10,"2333",2333,true),
            };
        foreach (var entity in entitys)
            await Repository.AddAsync(entity);
        await Repository.SaveAsync();
        var data = await Repository.FindAsync(item => item.Id < 5);
        Assert.True(data.Count() == 4);
        data = await TestRepository.FindAsync(item => true);
    }

    [Fact, Order(12)]
    public async Task QueryRepositoryIsExistTest()
    {
        Assert.True(await Repository.IsExistAsync(item => item.Id == 10));
        Assert.True(await Repository.IsExistAsync(item => item.Number <= 2333));
        Assert.True(await Repository.IsExistAsync(item => item.Code == "2333"));
        Assert.False(await Repository.IsExistAsync(item => item.Id == 111));
        Assert.False(await Repository.IsExistAsync(item => item.Number > 2333));
        Assert.False(await Repository.IsExistAsync(item => item.Code != "2333"));
    }

    [Fact, Order(13)]
    public async Task QueryRepositoryCountTest()
    {
        Assert.True((await Repository.CountAsync(item => item.Id > 4 && item.Id < 9)) == 4);
        Assert.False((await Repository.CountAsync(item => item.Id < 4)) == 4);
    }

    [Fact, Order(14)]
    public async Task QueryRepositoryQueryByIdTest()
    {
        var ids = new[] { 1, 3, 5, 7, 9 };
        var data = await Repository.FindAsync(item => ids.Contains(item.Id));
        Assert.True(data.Count() == 5);
        ids = new[] { 2, 6, 8 };
        data = await Repository.FindAsync(item => ids.Contains(item.Id));
        Assert.True(data.Count() == 3);
    }

    [Fact, Order(15)]
    public async Task QueryRepositoryQueryPageTest()
    {
        var data = await Repository.FindPageAsync(item => item.Id > 6);
        Assert.True(data.Length == 4);
        Assert.True(data.Data.First().Id == 7);
        Assert.True(data.Data.Last().Id == 10);
    }
    [Fact, Order(16)]
    public async Task QueryRepositoryQueryPageOrderTest()
    {
        var data = await Repository.FindPageAsync(item => true, null, item => item.Id, false);
        Assert.True(data.Data.First().Id == 10);
    }

    [Fact, Order(17)]
    public async Task QueryRepositoryRemoveAllTest()
    {
        var ids = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        foreach (var id in ids)
            await Repository.DeleteAsync(id, true);
        await Repository.SaveAsync();
        var data = await Repository.FindAsync(item => true);
        Assert.True(data.IsEmpty());
    }
}
