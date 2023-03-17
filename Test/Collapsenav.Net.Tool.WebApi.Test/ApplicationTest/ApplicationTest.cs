using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Collapsenav.Net.Tool.WebApi.Test;

[TestCaseOrderer("Collapsenav.Net.Tool.WebApi.Test.TestOrders", "Collapsenav.Net.Tool.WebApi.Test")]
public class ApplicationTest
{
    public ApplicationTest()
    {
        Provider = DIConfig.GetAppProvider();
    }
    protected readonly IServiceProvider Provider;
    protected T GetService<T>()
    {
        return Provider.GetService<T>();
    }

    public class TestEntityReturnDto
    {
        public string Code { get; set; }
        public int? Number { get; set; }
    }
    [Fact]
    public async Task MapQueryTest()
    {
        var app = GetService<ICrudApplication<int, TestEntity, TestEntityCreate, TestEntityGet>>();
        var data = await app.QueryAsync<TestEntityReturnDto>(new TestEntityGet());
        Assert.True(data.Count() == 10);
        var item = await app.QueryByStringIdAsync("1");
        Assert.True(item.Id == 1);
    }

    public class NewGetInput : BaseGet<TestEntity>
    {
        public override IQueryable<TestEntity> GetQuery(IQueryable<TestEntity> query)
        {
            return query;
        }
    }

    [Fact, Order(333)]
    public async Task NewGetInputTest()
    {
        var app = GetService<ICrudApplication<int, TestEntity, TestEntityCreate, TestEntityGet>>();
        var data = await app.QueryAsync(new NewGetInput());
        Assert.True(data.Count() == 10);

        data = await app.GetQuery(new NewGetInput()).ToListAsync();
        Assert.True(data.Count() == 10);
    }

    [Fact]
    public async Task NewGetInputMapQueryTest()
    {
        var app = GetService<ICrudApplication<int, TestEntity, TestEntityCreate, TestEntityGet>>();
        var data = await app.QueryAsync<NewGetInput, TestEntityReturnDto>(new NewGetInput());
        Assert.True(data.Count() == 10);
    }

    [Fact]
    public async Task CrudApplicationUpdateTest()
    {
        var app = GetService<ICrudApplication<int, TestEntity, TestEntityCreate, TestEntityGet>>();
        await app.UpdateAsync("1", new TestEntity { Code = "32" });
        await app.SaveAsync();
        var datas = await app.GetQuery(new TestEntityGet { Code = "32" }).ToListAsync();
        Assert.True(datas.Count == 1);
        await app.UpdateAsync("1", new TestEntity { Code = "99999" });
        app.Save();
        datas = await app.GetQuery(new TestEntityGet { Code = "99999" }).ToListAsync();
        Assert.True(datas.Count == 1);
    }

    [Fact]
    public async Task CheckExistApplicationTest()
    {
        var app = GetService<ICheckExistApplication<TestEntity>>();
        Assert.True(await app.IsExistAsync(item => true));
        Assert.False(await app.IsExistAsync(item => item.Id > 9999));
    }

    [Fact]
    public async Task CountApplicationTest()
    {
        var app = GetService<ICountApplication<TestEntity>>();
        Assert.True(await app.CountAsync(item => true) > 0);
        Assert.False(await app.CountAsync(item => item.Id > 9999) > 0);
    }
}