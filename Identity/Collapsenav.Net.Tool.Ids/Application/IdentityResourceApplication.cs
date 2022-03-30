using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace Collapsenav.Net.Tool.Ids;

#if NETCOREAPP3_1_OR_GREATER
public class IdentityResourceApplication : IIdentityResourceApplication
{
    private readonly ConfigurationDbContext _context;
    private DbSet<IdentityResource> dbSet;
    public IdentityResourceApplication(ConfigurationDbContext context)
    {
        _context = context;
        dbSet = _context.IdentityResources;
    }

    public virtual async Task<IdentityResource> AddAsync(IdentityResourceDto entity)
    {
        var scope = entity.ToEntity();
        await dbSet.AddAsync(scope);
        var dd = await SaveAsync();
        return scope;
    }

    public virtual async Task<IdentityResource> AddAsync(IdentityResource entity)
    {
        await dbSet.AddAsync(entity);
        await SaveAsync();
        return entity;
    }

    public virtual async Task<int> AddRangeAsync(IEnumerable<IdentityResourceDto> entitys)
    {
        var scopes = entitys.Select(item => item.ToEntity());
        await dbSet.AddRangeAsync(scopes);
        return entitys.Count();
    }

    public virtual async Task<bool> DeleteAsync(int id, bool isTrue = false)
    {
        if (isTrue)
            return (await dbSet.DeleteByKeyAsync(id)) > 0;
        return true;
    }
    public virtual async Task<bool> DeleteAsync(string id, bool isTrue = false)
    {
        if (isTrue)
            return (await dbSet.DeleteByKeyAsync(id)) > 0;
        return true;
    }

    public virtual async Task<int> DeleteRangeAsync(IEnumerable<int> id, bool isTrue = false)
    {
        if (isTrue)
            return await dbSet.Where(item => id.Contains(item.Id)).DeleteAsync();
        return 0;
    }

    public void Dispose()
    {
    }

    public IQueryable<IdentityResource> GetQuery(IdentityResourceGet input)
    {
        return dbSet.AsNoTracking()
        .Include(item => item.UserClaims)
        .Include(item => item.Properties)
        ;
    }

    public virtual async Task<IEnumerable<IdentityResource>> QueryAsync(IdentityResourceGet input)
    {
        return GetQuery(input);
    }

    public virtual async Task<IdentityResource> QueryAsync(int id)
    {
        return await dbSet.FindAsync(id);
    }

    public virtual async Task<IEnumerable<IdentityResource>> QueryByIdsAsync(IEnumerable<int> ids)
    {
        return await dbSet.WhereIf(ids.NotEmpty(), item => ids.Contains(item.Id)).ToListAsync();
    }

    public virtual async Task<IdentityResource> QueryByStringIdAsync(string id)
    {
        return await dbSet.FindAsync(id);
    }

    public virtual async Task<PageData<IdentityResource>> QueryPageAsync(IdentityResourceGet input, PageRequest page = null)
    {
        return new PageData<IdentityResource>
        {
            Data = await QueryAsync(input)
        };
    }

    public virtual async Task<int> UpdateAsync(int id, IdentityResourceDto entity)
    {
        return await UpdateAsync(id.ToString(), entity.ToEntity());
    }

    public virtual async Task<int> UpdateAsync(string id, IdentityResource entity)
    {
        entity.Id = int.Parse(id);
        dbSet.Update(entity);
        return await SaveAsync();
    }
    private int Save()
    {
        return _context.SaveChanges();
    }
    private async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }
}
#endif
