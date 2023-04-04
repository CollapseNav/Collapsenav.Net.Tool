using System.Data;
using NPOI.SS.UserModel;

namespace Collapsenav.Net.Tool.Excel;

public partial class NPOITool
{
    /// <summary>
    /// 获取 NPOI中使用 的 Workbook
    /// </summary>
    public static IWorkbook NPOIWorkbook(string path)
    {
        using var notCloseStream = new NPOINotCloseStream(path);
        return notCloseStream.GetWorkBook();
    }
    /// <summary>
    /// 获取 NPOI中使用 的 Workbook
    /// </summary>
    public static IWorkbook NPOIWorkbook(Stream stream)
    {
        using var notCloseStream = stream is NPOINotCloseStream nstream ? nstream : new NPOINotCloseStream(stream);
        return notCloseStream.GetWorkBook();
    }
    /// <summary>
    /// 获取 NPOI中使用 的 Sheet
    /// </summary>
    public static ISheet NPOISheet(string path, string sheetname = null)
    {
        var workbook = NPOIWorkbook(path);
        return sheetname.IsNull() ? workbook.GetSheetAt(ExcelTool.NPOIZero) : workbook.GetSheet(sheetname);
    }
    /// <summary>
    /// 获取 NPOI中使用 的 Sheet
    /// </summary>
    public static ISheet NPOISheet(Stream stream, string sheetname = null)
    {
        var workbook = NPOIWorkbook(stream);
        if (workbook.NumberOfSheets == 0)
            return null;
        return sheetname.IsNull() ? workbook.GetSheetAt(ExcelTool.NPOIZero) : workbook.GetSheet(sheetname);
    }
    /// <summary>
    /// 获取 NPOI中使用 的 Sheet
    /// </summary>
    public static ISheet NPOISheet(string path, int sheetindex)
    {
        var workbook = NPOIWorkbook(path);
        return workbook.GetSheetAt(sheetindex);
    }
    /// <summary>
    /// 获取 NPOI中使用 的 Sheet
    /// </summary>
    public static ISheet NPOISheet(Stream stream, int sheetindex)
    {
        var workbook = NPOIWorkbook(stream);
        return workbook.GetSheetAt(sheetindex);
    }

    /// <summary>
    /// 获取表格header(仅限简单的单行表头)
    /// </summary>
    /// <param name="sheet">工作簿</param>
    public static IEnumerable<string> ExcelHeader(ISheet sheet)
    {
        var header = sheet.GetRow(ExcelTool.NPOIZero)?.Cells.Select(item => item.ToString()?.Trim());
        return header;
    }
    /// <summary>
    /// 获取表格header和对应的index
    /// </summary>
    public static IDictionary<string, int> HeadersWithIndex(ISheet sheet)
    {
        var headers = sheet.GetRow(ExcelTool.NPOIZero)?.Cells
        .Where(item => item.ToString().NotNull())
        .ToDictionary(item => item.ToString()?.Trim(), item => item.ColumnIndex);
        return headers;
    }
}