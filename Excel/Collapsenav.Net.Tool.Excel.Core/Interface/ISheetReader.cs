namespace Collapsenav.Net.Tool.Excel;

public interface ISheetReader<T> where T : IExcelContainer<object>
{
    Stream SheetStream { get; }
    IEnumerable<T> Readers { get; }
    IDictionary<string, T> Sheets { get; }
    T this[int index] { get; }
    T this[string sheetName] { get; }
}