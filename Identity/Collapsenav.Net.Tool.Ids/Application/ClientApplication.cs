using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace Collapsenav.Net.Tool.Ids;

public class ClientApplication : IClientApplication
{
    private readonly ConfigurationDbContext _context;
    private DbSet<Client> dbSet;
    public ClientApplication(ConfigurationDbContext context)
    {
        _context = context;
        dbSet = _context.Clients;
    }

    public virtual async Task<Client> AddAsync(ClientDto input)
    {
        var client = input.ToItem();
        var isExist = await IsExist(client.ClientId);
        if (isExist)
            throw new Exception("ClientId 重复");
        return await AddAsync(client.ToEntity());
    }

    public virtual async Task<Client> AddAsync(Client entity)
    {
        await dbSet.AddAsync(entity);
        await SaveAsync();
        return entity;
    }

    public virtual async Task<int> AddRangeAsync(IEnumerable<ClientDto> entitys)
    {
        var isExist = (await QueryByIdsAsync(entitys.Select(item => item.Id.Value)))?.Count() > 0;
        if (isExist)
            throw new Exception("ClientId 重复");
        await dbSet.AddRangeAsync(entitys.Select(item => item.ToEntity()));
        return entitys.Count();
    }

    public virtual async Task<bool> DeleteAsync(int id, bool isTrue = false)
    {
        if (isTrue)
            return (await dbSet.DeleteByKeyAsync(id)) > 0;
        return (await dbSet.UpdateAsync(item => id == item.Id, entity => new Client { Enabled = false })) > 0;
    }
    public virtual async Task<bool> DeleteAsync(string id, bool isTrue = false)
    {
        if (isTrue)
            return (await dbSet.DeleteByKeyAsync(id)) > 0;
        return (await dbSet.UpdateAsync(item => int.Parse(id) == item.Id, entity => new Client { Enabled = false })) > 0;
    }

    public virtual async Task<int> DeleteRangeAsync(IEnumerable<int> id, bool isTrue = false)
    {
        if (isTrue)
            return await dbSet.Where(item => id.Contains(item.Id)).DeleteAsync();
        return await dbSet.UpdateAsync(item => id.Contains(item.Id), entity => new Client { Enabled = false });
    }

    public void Dispose()
    {
    }

    public virtual async Task<Client> GetAsync(string clientId)
    {
        var client = await GetQuery(null)
        .FirstOrDefaultAsync(item => item.ClientId == clientId);
        return client;
    }

    public IQueryable<Client> GetQuery(ClientGet input)
    {
        var query = dbSet
        .WhereIf(input.ClientId, item => item.ClientId == input.ClientId)
        .Include(item => item.AllowedScopes)
        .Include(item => item.RedirectUris)
        .Include(item => item.PostLogoutRedirectUris)
        .Include(item => item.AllowedGrantTypes)
        .Include(item => item.AllowedCorsOrigins)
        .Include(item => item.ClientSecrets)
        .Include(item => item.Claims)
        .AsNoTracking()
        ;
        return query;
    }

    public virtual async Task<bool> IsExist(string clientId)
    {
        return await dbSet.AsNoTracking().AnyAsync(item => item.ClientId == clientId);
    }
    public virtual async Task<bool> IsExist(IEnumerable<string> clientId)
    {
        return await dbSet.AsNoTracking().AnyAsync(item => clientId.Contains(item.ClientId));
    }

    public virtual async Task<IEnumerable<Client>> QueryAsync(ClientGet input)
    {
        return await GetQuery(input).ToListAsync();
    }

    public virtual async Task<Client> QueryAsync(int id)
    {
        return await dbSet.FindAsync(id);
    }

    public virtual async Task<IEnumerable<Client>> QueryByIdsAsync(IEnumerable<int> ids)
    {
        return await GetQuery(null)
        .WhereIf(ids.NotEmpty(), item => ids.Contains(item.Id))
        .ToListAsync()
        ;
    }

    public virtual async Task<Client> QueryByStringIdAsync(string id)
    {
        return await GetAsync(id);
    }

    public virtual async Task<PageData<Client>> QueryPageAsync(ClientGet input, PageRequest page = null)
    {
        return new PageData<Client>
        {
            Data = await QueryAsync(input)
        };
    }

    public virtual async Task<int> UpdateApiScopeAsync(string clientId, IEnumerable<string> input)
    {
        if (!await IsExist(clientId))
            throw new Exception("ClientId 不存在");
        var client = await GetAsync(clientId);
        client.AllowedScopes = new ClientDto { AllowedScopes = input.ToList() }.ToEntity().AllowedScopes;
        return await UpdateAsync(client);
    }

    public virtual async Task<int> UpdateAsync(ClientDto input)
    {
        return await UpdateAsync(input.ToItem());
    }

    public virtual async Task<int> UpdateAsync(Client input)
    {
        dbSet.Update(input);
        return await SaveAsync();
    }

    public virtual async Task<int> UpdateAsync(IdentityServer4.Models.Client input)
    {
        return await UpdateAsync(input.ToEntity());
    }

    public virtual async Task<int> UpdateAsync(int id, ClientDto entity)
    {
        entity.Id = id;
        return await UpdateAsync(entity);
    }

    public virtual async Task<int> UpdateAsync(string id, Client entity)
    {
        entity.ClientId = id;
        return await UpdateAsync(entity);
    }

    public virtual async Task<int> UpdateCorsOriginsAsync(string clientId, IEnumerable<string> input)
    {
        if (!await IsExist(clientId))
            throw new Exception("ClientId 不存在");
        var client = await GetAsync(clientId);
        client.AllowedCorsOrigins = new ClientDto { AllowedCorsOrigins = input.ToList() }.ToEntity().AllowedCorsOrigins;
        return await UpdateAsync(client);
    }

    public virtual async Task<int> UpdateGrantTypeAsync(string clientId, string input)
    {
        if (!await IsExist(clientId))
            throw new Exception("ClientId 不存在");
        var client = await GetAsync(clientId);
        client.AllowedGrantTypes = new ClientDto { ClientType = input }.ToEntity().AllowedGrantTypes;
        return await UpdateAsync(client);
    }

    public virtual async Task<int> UpdateLogoutRedirectUriAsync(string clientId, IEnumerable<string> input)
    {
        if (!await IsExist(clientId))
            throw new Exception("ClientId 不存在");
        var client = await GetAsync(clientId);
        client.PostLogoutRedirectUris = new ClientDto { PostLogoutRedirectUris = input.ToList() }.ToEntity().PostLogoutRedirectUris;
        return await UpdateAsync(client);
    }

    public virtual async Task<int> UpdateRedirectUriAsync(string clientId, IEnumerable<string> input)
    {
        if (!await IsExist(clientId))
            throw new Exception("ClientId 不存在");
        var client = await GetAsync(clientId);
        client.RedirectUris = new ClientDto { RedirectUris = input.ToList() }.ToEntity().RedirectUris;
        return await UpdateAsync(client);
    }

    public virtual async Task<int> UpdateTokenLifeTimeAsync(string clientId, int minute)
    {
        if (!await IsExist(clientId))
            throw new Exception("ClientId 不存在");
        var client = await GetAsync(clientId);
        client.AccessTokenLifetime = new ClientDto { TokenLifeTime = minute }.ToEntity().AccessTokenLifetime;
        return await UpdateAsync(client);
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

