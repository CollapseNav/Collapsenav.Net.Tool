namespace Collapsenav.Net.Tool;

/// <summary>
/// 自定义的comparer
/// </summary>
public class CollapseNavEqualityComparer<T> : IEqualityComparer<T>
{
    public CollapseNavEqualityComparer(Func<T, int> hashcodeFunc)
    {
        HashCodeFunc = hashcodeFunc;
    }
    public CollapseNavEqualityComparer(Func<T, T, bool> equalFunc, Func<T, int> hashcodeFunc = null)
    {
        EqualFunc = equalFunc;
        HashCodeFunc = hashcodeFunc;
    }
    private readonly Func<T, T, bool> EqualFunc;
    private readonly Func<T, int> HashCodeFunc;

    public bool Equals(T x, T y)
    {
        return EqualFunc == null ? GetHashCode(x) == GetHashCode(y) : EqualFunc(x, y);
    }

    public int GetHashCode(T obj)
    {
        return HashCodeFunc == null ? 0 : HashCodeFunc(obj);
    }
}
