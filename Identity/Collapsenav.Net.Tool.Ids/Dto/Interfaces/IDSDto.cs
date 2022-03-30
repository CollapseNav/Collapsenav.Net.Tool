namespace Collapsenav.Net.Tool.Ids;

public interface IDSDto<T, EntityT>
{
    T ToItem();
    EntityT ToEntity();
}