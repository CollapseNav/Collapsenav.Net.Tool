using NPOI.SS.UserModel;
using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel;
/// <summary>
/// 尝试使用 IColumnRead 统一 NPOI 和 EPPlus 的调用
/// </summary>
public interface IExcelRead
{
    IEnumerable<string> this[string field] { get; }
    IEnumerable<string> this[long col] { get; }
    string this[long row, long col] { get; }
    string this[string field, long row] { get; }
    int Zero { get; }
    long RowCount { get; }
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
    IEnumerable<string> Headers { get; }
    IDictionary<string, int> HeadersWithIndex { get; }
}
