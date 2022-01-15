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
    IEnumerable<T> this[string field] { get; }
    IEnumerable<T> this[long row] { get; }
    T this[long row, long col] { get; }
    T this[string field, long row] { get; }
    IEnumerable<string> Headers { get; }
    IDictionary<string, int> HeadersWithIndex { get; }
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
    /// <summary>
    /// 根据文件大小选择"合适"的 IExcelRead
    /// 5M以下随机选 EPPlus 或 NPOI
    /// 5M以上选 MiniExcel
    /// </summary>
    public static IExcelRead GetExcelRead(Stream stream, ExcelType? excelType = null)
    {
        excelType ??= stream.Length switch
        {
            >= 5 * 1024 * 1024 => ExcelType.MiniExcel,
            <= 5 * 1024 * 1024 => new Random().Next() % 2 == 0 ? ExcelType.EPPlus : ExcelType.NPOI,
        };
        IExcelRead read = excelType switch
        {
            ExcelType.EPPlus => new EPPlusExcelRead(stream, 1),
            ExcelType.NPOI => new NPOIExcelRead(stream, 1),
            ExcelType.MiniExcel => new MiniExcelRead(stream, 1),
            _ => new MiniExcelRead(stream, 1)
        };
        return read;
    }
    /// <summary>
    /// 根据文件大小选择"合适"的 IExcelRead
    /// 5M以下随机选 EPPlus 或 NPOI
    /// 5M以上选 MiniExcel
    /// </summary>
    public static IExcelRead GetExcelRead(string path, ExcelType? excelType = null)
    {
        using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        return GetExcelRead(path, excelType);
    }
    long RowCount { get; }
}

public enum ExcelType
{
    NPOI,
    EPPlus,
    MiniExcel
}
