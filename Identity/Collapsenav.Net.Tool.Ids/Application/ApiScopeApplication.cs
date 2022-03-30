using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace Collapsenav.Net.Tool.Ids;

#if NETCOREAPP3_1_OR_GREATER
public class ApiScopeApplication : IApiScopeApplication
{
    private readonly ConfigurationDbContext _context;
    private DbSet<ApiScope> dbSet;
    public ApiScopeApplication(ConfigurationDbContext context)
    {
        _context = context;
        dbSet = _context.ApiScopes;
    }

    public virtual async Task<ApiScope> AddAsync(ApiScopeDto entity)
    {
        var scope = entity.ToEntity();
        await dbSet.AddAsync(scope);
        await SaveAsync();
        return scope;
    }

    public virtual async Task<ApiScope> AddAsync(ApiScope entity)
    {
        await dbSet.AddAsync(entity);
        await SaveAsync();
        return entity;
    }

    public virtual async Task<int> AddRangeAsync(IEnumerable<ApiScopeDto> entitys)
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

    public IQueryable<ApiScope> GetQuery(ApiScopeGet input)
    {
        return dbSet.AsNoTracking()
        .Include(item => item.UserClaims)
        .Include(item => item.Properties)
        ;
    }

    public virtual async Task<IEnumerable<ApiScope>> QueryAsync(ApiScopeGet input)
    {
        return GetQuery(input);
    }

    public virtual async Task<ApiScope> QueryAsync(int id)
    {
        return await dbSet.FindAsync(id);
    }

    public virtual async Task<IEnumerable<ApiScope>> QueryByIdsAsync(IEnumerable<int> ids)
    {
        return await dbSet.WhereIf(ids.NotEmpty(), item => ids.Contains(item.Id)).ToListAsync();
    }

    public virtual async Task<ApiScope> QueryByStringIdAsync(string id)
    {
        return await dbSet.FindAsync(id);
    }

    public virtual async Task<PageData<ApiScope>> QueryPageAsync(ApiScopeGet input, PageRequest page = null)
    {
        return new PageData<ApiScope>
        {
            Data = await QueryAsync(input)
        };
    }

    public virtual async Task<int> UpdateAsync(int id, ApiScopeDto entity)
    {
        return await UpdateAsync(id.ToString(), entity.ToEntity());
    }

    public virtual async Task<int> UpdateAsync(string id, ApiScope entity)
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
