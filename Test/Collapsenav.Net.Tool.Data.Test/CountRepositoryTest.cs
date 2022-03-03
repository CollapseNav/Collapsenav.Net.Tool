using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Collapsenav.Net.Tool.Data.Test;
[TestCaseOrderer("Collapsenav.Net.Tool.Data.Test.TestOrders", "Collapsenav.Net.Tool.Data.Test")]
public class CountRepositoryTest
{
    protected readonly IServiceProvider Provider;
    protected readonly ICountRepository<TestModifyEntity> Repository;
    public CountRepositoryTest()
    {
        Provider = DIConfig.GetProvider();
        Repository = GetService<ICountRepository<TestModifyEntity>>();
    }
    protected T GetService<T>()
    {
        return Provider.GetService<T>();
    }

    [Fact, Order(13)]
    public async Task QueryRepositoryCountTest()
    {
        Assert.True((await Repository.CountAsync(item => item.Id > 4 && item.Id < 9)) == 4);
        Assert.False((await Repository.CountAsync(item => item.Id < 4)) == 4);
    }
}
