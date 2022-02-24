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
        return typeof(T).Props().Select(item => new DiffItem<T>(before, end, item)).Where(item => item.PropName.NotEmpty()).ToList();
    }
}
public class DiffItem<T>
{
    public DiffItem() { }
    public DiffItem(T before, T end, PropertyInfo prop)
    {
        var l = before.GetValue(prop.Name);
        var r = end.GetValue(prop.Name);
        if (l.Equals(r))
            return;
        Prop = prop;
        Bvalue = l;
        Evalue = r;
    }
    public PropertyInfo Prop { get; set; }
    public string PropName { get => Prop?.Name; }
    public object Bvalue { get; set; }
    public object Evalue { get; set; }
}


