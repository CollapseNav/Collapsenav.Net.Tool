namespace Collapsenav.Net.Tool.Excel;

/// <summary>
/// 多sheet读取
/// </summary>
public interface ISheetReader<T> where T : IExcelContainer<object>
{
    /// <summary>
    /// 文件流
    /// </summary>
    Stream SheetStream { get; }
    /// <summary>
    /// reader集合
    /// </summary>
    IEnumerable<T> Readers { get; }
    /// <summary>
    /// sheet名称和reader的字典
    /// </summary>
    IDictionary<string, T> Sheets { get; }
    T this[int index] { get; }
    T this[string sheetName] { get; }
}