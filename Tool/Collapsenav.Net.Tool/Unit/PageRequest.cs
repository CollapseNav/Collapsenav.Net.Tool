namespace Collapsenav.Net.Tool;
public class PageRequest
{
    public virtual int Index { get; set; } = 1;
    public virtual int Max { get; set; } = 20;
    /// <summary>
    /// skip = (index - 1) * max
    /// </summary>
    /// <value></value>
    public virtual int Skip
    {
        get => skip ?? (Index - 1) * Max; set => skip = value;
    }
    protected int? skip;
}
