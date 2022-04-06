namespace Collapsenav.Net.Tool.Data;
public interface IEntity
{
    void Init();
    void InitModifyId();
    void SoftDelete();
    void Update();
    Type KeyType();
}
public interface IEntity<TKey> : IEntity
{
}