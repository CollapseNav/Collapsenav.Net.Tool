using Collapsenav.Net.Tool.Data;
using Microsoft.EntityFrameworkCore;

namespace DataDemo.EntityLib;

public class EntityContext : DbContext
{
    public DbSet<FirstEntity> FirstEntity { get; set; }
    public DbSet<SecondEntity> SecondEntity { get; set; }
    public EntityContext(DbContextOptions<EntityContext> options) : base(options)
    {
    }
}