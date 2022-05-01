using Collapsenav.Net.Tool.Data;
namespace Collapsenav.Net.Tool.WebApi;

public class Application<T> : IApplication<T> where T : IEntity
{
    protected IRepository<T> Repo;
    public Application(IRepository<T> repository)
    {
        Repo = repository;
    }
}
