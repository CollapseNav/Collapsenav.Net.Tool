using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Collapsenav.Net.Tool.Data.Test;
public class TestRepository : Repository<int, TestEntity>
{
    public TestRepository(DbContext db) : base(db)
    {
    }
    public override async Task<TestEntity> FindAsync(int id)
    {
        var data = await base.FindAsync(id);
        data.Number += 100;
        return data;
    }
}
public class TestQueryRepository : QueryRepository<TestEntity>
{
    public TestQueryRepository(DbContext db) : base(db)
    {
    }
    public override async Task<TestEntity> FindAsync<TKey>(TKey id)
    {
        var data = await base.FindAsync(id);
        data.Number += 200;
        return data;
    }
}
public class TestModifyRepository : ModifyRepository<TestEntity>
{
    public TestModifyRepository(DbContext db) : base(db)
    {
    }
    public override async Task<TestEntity> FindAsync<TKey>(TKey id)
    {
        var data = await base.FindAsync(id);
        data.Number += 300;
        return data;
    }
}
