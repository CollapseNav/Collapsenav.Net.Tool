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
