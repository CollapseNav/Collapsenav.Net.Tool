using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Collapsenav.Net.Tool.Data.Test;
[TestCaseOrderer("Collapsenav.Net.Tool.Data.Test.TestOrders", "Collapsenav.Net.Tool.Data.Test")]
public class CheckExistRepositoryTest
{
    protected readonly IServiceProvider Provider;
    protected readonly ICheckExistRepository<TestEntity> Repository;
    public CheckExistRepositoryTest()
    {
        Provider = DIConfig.GetProvider();
        Repository = GetService<ICheckExistRepository<TestEntity>>();
    }
    protected T GetService<T>()
    {
        return Provider.GetService<T>();
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
}
