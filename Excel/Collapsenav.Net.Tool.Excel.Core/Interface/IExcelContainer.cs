namespace Collapsenav.Net.Tool.Excel;

public interface IExcelContainer<out T> : IDisposable, IEnumerable<IEnumerable<T>>
{
    IEnumerable<T> this[string field] { get; }
    IEnumerable<T> this[int row] { get; }
    T this[int row, int col] { get; }
    T this[string field, int row] { get; }
    IEnumerable<string> Headers { get; }
    IDictionary<string, int> HeadersWithIndex { get; }
    int RowCount { get; }
    int Zero { get; }
}