namespace Collapsenav.Net.Tool.Excel;

public interface IExcelContainer<T> : IDisposable, IEnumerable<IEnumerable<T>>
{
    IEnumerable<T> this[string field] { get; }
    IEnumerable<T> this[long row] { get; }
    T this[long row, long col] { get; }
    T this[string field, long row] { get; }
    IEnumerable<string> Headers { get; }
    IDictionary<string, int> HeadersWithIndex { get; }
    long RowCount { get; }
    int Zero { get; }
}