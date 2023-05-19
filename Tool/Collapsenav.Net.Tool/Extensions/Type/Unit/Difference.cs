using System.Reflection;

namespace Collapsenav.Net.Tool;

public class Difference<T>
{
    public Difference(T before, T end)
    {
        Before = before;
        End = end;
        Diffs = Comparer(Before, End);
    }
    public IEnumerable<DiffItem<T>> Diffs { get; private set; }
    public T Before { get; private set; }
    public T End { get; private set; }
    public DiffItem<T> GetDiff(string propName)
    {
        return Diffs.FirstOrDefault(item => item.PropName == propName);
    }
    public DiffItem<T> GetDiff(PropertyInfo prop)
    {
        return Diffs.FirstOrDefault(item => item.Prop == prop);
    }
    public static IEnumerable<DiffItem<T>> Comparer(T before, T end)
    {
        return typeof(T).Props().Select(item => new DiffItem<T>(before, end, item))
        .Where(item => item.PropName.NotEmpty()).ToList();
    }
}
/// <summary>
/// 差异item
/// </summary>
public class DiffItem<T>
{
    public DiffItem() { }
    /// <summary>
    /// 根据属性初始化 before 和 end 的diff
    /// </summary>
    public DiffItem(T before, T end, PropertyInfo prop)
    {
        var l = before.GetValue(prop.Name);
        var r = end.GetValue(prop.Name);
        if (l.Equals(r))
            return;
        Prop = prop;
        Beforevalue = l;
        Endvalue = r;
    }

    public DiffItem(PropertyInfo prop, object before, object end)
    {
        Prop = prop;
        Beforevalue = before;
        Endvalue = end;
    }
    public PropertyInfo Prop { get; set; }
    public string PropName { get => Prop?.Name; }
    public object Beforevalue { get; set; }
    public object Endvalue { get; set; }

    public static IEnumerable<DiffItem<T>> GetDiffs(T before, T end)
    {
        var props = before.GetType().Props().GetItemIn(end.GetType().Props(), (a, b) => a.Name == b.Name && a.PropertyType == b.PropertyType).ToArray();
        if (props.IsEmpty())
            return default;
        return props.Select(item => new DiffItem<T>(item, item.GetValue(before), item.GetValue(end)));
    }
}


