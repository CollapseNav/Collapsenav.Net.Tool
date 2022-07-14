namespace Collapsenav.Net.Tool;
public class PageData : PageData<object>
{
}
public class PageData<T>
{
    public int? Total { get; set; }
    public int? Length { get => Data?.Count(); }
    public IEnumerable<T> Data { get => _data; set => _data = value; }
    private IEnumerable<T> _data;
    public PageData() { }
    public PageData(int? total, IEnumerable<T> data) : this(data, total)
    {
    }
    public PageData(IEnumerable<T> data, int? total = null)
    {
        Total = total ?? data?.Count();
        Data = data;
    }
}
