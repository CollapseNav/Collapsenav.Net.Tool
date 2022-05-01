using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Collapsenav.Net.Tool.Data.Test;
[TestCaseOrderer("Collapsenav.Net.Tool.Data.Test.TestOrders", "Collapsenav.Net.Tool.Data.Test")]
public class NotBaseQueryRepositoryTest
{
    protected readonly IServiceProvider Provider;
    protected readonly IQueryRepository<TestNotBaseEntity> Repository;
    public NotBaseQueryRepositoryTest()
    {
        Provider = DIConfig.GetNotBaseProvider();
        Repository = GetService<IQueryRepository<TestNotBaseEntity>>();
    }
    protected T GetService<T>()
    {
        return Provider.GetService<T>();
    }

    [Fact, Order(11)]
    public async Task QueryRepositoryQueryTest()
    {
        var datas = await Repository.QueryAsync(item => item.Id < 5);
        Assert.True(datas.Count() == 4);
        var data = await Repository.GetByIdAsync(1);
        Assert.True(data.Number == 85);
    }

    [Fact, Order(12)]
    public async Task QueryRepositoryIsExistTest()
    {
        Assert.True(await Repository.IsExistAsync(item => item.Id == 10));
        Assert.True(await Repository.IsExistAsync(item => item.Number <= 2333));
        Assert.True(await Repository.IsExistAsync(item => item.Code.Contains("yxm")));
        Assert.False(await Repository.IsExistAsync(item => item.Id == 111));
        Assert.False(await Repository.IsExistAsync(item => item.Number > 2333));
        Assert.False(await Repository.IsExistAsync(item => item.Code == "2333"));
    }

    [Fact, Order(13)]
    public async Task QueryRepositoryCountTest()
    {
        Assert.True((await Repository.CountAsync(item => item.Id > 4 && item.Id < 9)) == 4);
        Assert.False((await Repository.CountAsync(item => item.Id < 4)) == 4);
    }

    [Fact, Order(15)]
    public async Task QueryRepositoryQueryPageTest()
    {
        var data = await Repository.QueryPageAsync(item => item.Id > 6);
        Assert.True(data.Length == 4);
        Assert.True(data.Data.First().Id == 7);
        Assert.True(data.Data.Last().Id == 10);
    }
    [Fact, Order(16)]
    public async Task QueryRepositoryQueryPageOrderTest()
    {
        var data = await Repository.QueryPageAsync(item => true, item => item.Id, true);
        Assert.True(data.Data.Last().Id == 10);
        data = await Repository.QueryPageAsync(item => true, item => item.Id, false);
        Assert.True(data.Data.First().Id == 10);
    }
}
