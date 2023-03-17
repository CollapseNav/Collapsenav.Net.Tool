namespace Collapsenav.Net.Tool.WebApi;
public abstract class BaseGet : IBaseGet
{
    public IQueryable GetQuery(IQueryable query)
    {
        return query;
    }
}
public abstract class BaseGet<T> : BaseGet, IBaseGet<T>
{
    public abstract IQueryable<T> GetQuery(IQueryable<T> query);
}