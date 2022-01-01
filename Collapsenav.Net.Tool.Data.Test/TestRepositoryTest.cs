using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Collapsenav.Net.Tool.Data.Test;
[TestCaseOrderer("Collapsenav.Net.Tool.Data.Test.TestOrders", "Collapsenav.Net.Tool.Data.Test")]
public class TestRepositoryTest
{
    protected readonly IServiceProvider Provider;
    private readonly IRepository<int, TestEntity> Repository;
    private readonly IRepository<int, TestEntity> TestRepository;
    private readonly IQueryRepository<TestEntity> TestQueryRepository;
    private readonly IModifyRepository<TestEntity> TestModifyRepository;
    public TestRepositoryTest()
    {
        Provider = DIConfig.GetTestRepositoryProvider();
        Repository = GetService<IRepository<int, TestEntity>>();
        TestRepository = GetService<TestRepository>();
        TestQueryRepository = GetService<TestQueryRepository>();
        TestModifyRepository = GetService<TestModifyRepository>();
    }
    protected T GetService<T>()
    {
        return Provider.GetService<T>();
    }

    [Fact, Order(99)]
    public async Task RepositoryAddTest()
    {
        var entity = new TestEntity
        {
            Id = 23333,
            Code = "23333333",
            IsTest = true,
            Number = 99
        };
        await Repository.AddAsync(entity);
        await Repository.SaveAsync();
        var testdata = await TestRepository.FindAsync(23333);
        var querydata = await TestQueryRepository.FindAsync(23333);
        var moditydata = await TestModifyRepository.FindAsync(23333);
        Assert.True(testdata.Number == 199);
        Assert.True(querydata.Number == 299);
        Assert.True(moditydata.Number == 399);
        await Repository.DeleteAsync(23333);
        Repository.Save();
    }
}
