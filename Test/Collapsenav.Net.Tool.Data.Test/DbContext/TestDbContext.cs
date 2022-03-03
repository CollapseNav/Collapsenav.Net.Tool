using Microsoft.EntityFrameworkCore;

namespace Collapsenav.Net.Tool.Data.Test;
public class TestDbContext : DbContext
{
    public DbSet<TestEntity> Tests { get; set; }
    public DbSet<TestQueryEntity> TestQuerys { get; set; }
    public DbSet<TestModifyEntity> TestModifys { get; set; }
    public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
    {
    }
}

public class TestNotBaseDbContext : DbContext
{
    public DbSet<TestNotBaseEntity> TestNotBases { get; set; }
    public DbSet<TestNotBaseQueryEntity> TestNotBaseQuerys { get; set; }
    public DbSet<TestNotBaseModifyEntity> TestNotBaseModifys { get; set; }
    public TestNotBaseDbContext(DbContextOptions<TestNotBaseDbContext> options) : base(options)
    {
    }
}
