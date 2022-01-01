namespace Collapsenav.Net.Tool.WebApi;
public class BaseCreate : IBaseCreate
{
    public bool IsExist() => false;
}
public class BaseCreate<T> : BaseCreate, IBaseCreate<T>
{
    public virtual bool IsExist(IQueryable<T> rep)
    {
        return false;
    }
}
