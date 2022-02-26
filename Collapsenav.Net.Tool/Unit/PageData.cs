namespace Collapsenav.Net.Tool;
public class PageData : PageData<object>
{
}
public class PageData<T>
{
    public int? Total { get; set; }
    public int? Length { get => Data?.Count(); }
    public IEnumerable<T> Data { get; set; }
    public PageData() { }
    public PageData(int? total, IEnumerable<T> data)
    {
        Total = total;
        Data = data;
    }
    public PageData(IEnumerable<T> data)
    {
        Total = data.Count();
        Data = data;
    }
}
