using NPOI.SS.UserModel;
using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel;
/// <summary>
/// 尝试使用 IColumnRead 统一 NPOI 和 EPPlus 的调用
/// </summary>
public interface IExcelRead : IExcelRead<string>
{
}

public interface IExcelRead<T> : IDisposable
{
    IEnumerable<T> this[T field] { get; }
    IEnumerable<T> this[long row] { get; }
    T this[long row, long col] { get; }
    T this[T field, long row] { get; }
    IEnumerable<T> Headers { get; }
    IDictionary<T, int> HeadersWithIndex { get; }
    public static IExcelRead GetExcelRead(object sheet)
    {
        if (sheet is ISheet)
        {
            return new NPOIExcelRead(sheet as ISheet);
        }
        else if (sheet is ExcelWorksheet)
        {
            return new EPPlusExcelRead(sheet as ExcelWorksheet);
        }
        return null;
    }

    public static IExcelRead GetExcelRead(Stream stream)
    {
        var type = stream.Length switch
        {
            _ => ExcelType.MiniExcel
        };
        return null;
    }
    int Zero { get; }
    long RowCount { get; }
}

public enum ExcelType

{
    NPOI,
    EPPlus,
    MiniExcel
}
