using Microsoft.EntityFrameworkCore;

namespace Collapsenav.Net.Tool.Data.Test;
    public class TestDbContext : DbContext
    {
        public DbSet<TestEntity> Tests { get; set; }
        public DbSet<TestTwoEntity> TestTwos { get; set; }
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        {
        }
}
