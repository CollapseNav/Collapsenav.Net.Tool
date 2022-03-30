namespace Collapsenav.Net.Tool.WebApi;
public interface IBaseCreate
{
}
public interface IBaseCreate<T> : IBaseCreate
{
    bool IsExist(IQueryable<T> rep);
}
