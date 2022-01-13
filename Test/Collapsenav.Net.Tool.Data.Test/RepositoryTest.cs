using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Collapsenav.Net.Tool.Data.Test;
[TestCaseOrderer("Collapsenav.Net.Tool.Data.Test.TestOrders", "Collapsenav.Net.Tool.Data.Test")]
public class RepositoryTest
{
    protected readonly IServiceProvider Provider;
    private readonly IRepository<int, TestEntity> Repository;
    public RepositoryTest()
    {
        Provider = DIConfig.GetProvider();
        Repository = GetService<IRepository<int, TestEntity>>();
    }
    protected T GetService<T>()
    {
        return Provider.GetService<T>();
    }

    [Fact, Order(1)]
    public async Task RepositoryAddTest()
    {
        var entity = new TestEntity
        {
            Id = 2333,
            Code = "23333333",
            IsTest = true,
            Number = null
        };
        await Repository.AddAsync(entity);
        await Repository.SaveAsync();
        var data = await Repository.FindAsync(2333);
        Assert.True(data.Id == 2333);
        Assert.True(data.Code == "23333333");
        Assert.True(data.IsTest == true);
        Assert.True(data.Number == null);
    }

    [Fact, Order(2)]
    public async Task RepositoryUpdateTest()
    {
        var entity = new TestEntity
        {
            Id = 2333,
            Code = "23333333",
            IsTest = false,
            Number = 648
        };
        await Repository.UpdateAsync(entity);
        await Repository.SaveAsync();
        var data = await Repository.FindAsync(2333);
        Assert.True(data.Id == 2333);
        Assert.True(data.Code == "23333333");
        Assert.True(data.IsTest == false);
        Assert.True(data.Number == 648);
    }

    [Fact, Order(3)]
    public async Task RepositoryDeleteTest()
    {
        await Repository.DeleteAsync(2333);
        await Repository.SaveAsync();
        var data = await Repository.FindAsync(2333);
        Assert.True(data.IsDeleted);

        await Repository.DeleteAsync(2333, true);
        await Repository.SaveAsync();
        data = await Repository.FindAsync(2333);
        Assert.Null(data);
    }
}
