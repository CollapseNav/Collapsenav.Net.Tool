using System.Data;
using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel;

public partial class EPPlusTool
{
    /// <summary>
    /// 获取 EPPlus中使用 的 ExcelPackage
    /// </summary>
    public static ExcelPackage EPPlusPackage(Stream stream)
    {
        return new(stream);
    }
    public static ExcelPackage EPPlusPackage(string path)
    {
        using var fs = path.OpenReadShareStream();
        return new(fs);
    }
    /// <summary>
    /// 获取 EPPlus中使用 的 Workbook
    /// </summary>
    public static ExcelWorkbook EPPlusWorkbook(string path)
    {
        return EPPlusPackage(path)?.Workbook;
    }
    /// <summary>
    /// 获取 EPPlus中使用 的 Workbook
    /// </summary>
    public static ExcelWorkbook EPPlusWorkbook(Stream stream)
    {
        return EPPlusPackage(stream)?.Workbook;
    }
    /// <summary>
    /// 获取 EPPlus中使用 的 Sheets
    /// </summary>
    public static ExcelWorksheets EPPlusSheets(string path)
    {
        var sheets = EPPlusWorkbook(path)?.Worksheets;
        return sheets.Count > 0 ? sheets : null;
    }
    /// <summary>
    /// 获取 EPPlus中使用 的 Sheets
    /// </summary>
    public static ExcelWorksheets EPPlusSheets(Stream stream)
    {
        var sheets = EPPlusWorkbook(stream)?.Worksheets;
        return sheets.Count > 0 ? sheets : null;
    }
    /// <summary>
    /// 获取 EPPlus中使用 的 Sheet
    /// </summary>
    public static ExcelWorksheet EPPlusSheet(string path, string sheetname = null)
    {
        var sheets = EPPlusSheets(path);
        return sheetname.IsNull() ? sheets?[ExcelTool.EPPlusZero] : sheets?[sheetname];
    }
    /// <summary>
    /// 获取 EPPlus中使用 的 Sheet
    /// </summary>
    public static ExcelWorksheet EPPlusSheet(Stream stream, string sheetname = null)
    {
        var sheets = EPPlusSheets(stream);
        return sheetname.IsNull() ? sheets?[ExcelTool.EPPlusZero] : sheets?[sheetname];
    }
    /// <summary>
    /// 获取 EPPlus中使用 的 Sheet
    /// </summary>
    public static ExcelWorksheet EPPlusSheet(string path, int sheetindex)
    {
        var sheets = EPPlusSheets(path);
        return sheets?[sheetindex];
    }
    /// <summary>
    /// 获取 EPPlus中使用 的 Sheet
    /// </summary>
    public static ExcelWorksheet EPPlusSheet(Stream stream, int sheetindex)
    {
        var sheets = EPPlusSheets(stream);
        return sheets?[sheetindex];
    }
    /// <summary>
    /// 将表格数据转换为T类型的集合
    /// </summary>
    public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(IExcelReader reader, ReadConfig<T> config = null)
    {
        config ??= ReadConfig<T>.GenDefaultConfig();
        return await config.ToEntityAsync(reader);
    }
    /// <summary>
    /// 获取表格header(仅限简单的单行表头)
    /// </summary>
    /// <param name="sheet">工作簿</param>
    public static IEnumerable<string> ExcelHeader(ExcelWorksheet sheet)
    {
        return sheet.Cells[ExcelTool.EPPlusZero, ExcelTool.EPPlusZero, ExcelTool.EPPlusZero, sheet.Dimension.Columns]
                .Select(item => item.Value?.ToString().Trim()).ToList();
    }
    /// <summary>
    /// 获取表格header和对应的index
    /// </summary>
    public static IDictionary<string, int> HeadersWithIndex(ExcelWorksheet sheet)
    {
        var headers = sheet.Cells[ExcelTool.EPPlusZero, ExcelTool.EPPlusZero, ExcelTool.EPPlusZero, sheet.Dimension.Columns]
        .Where(item => item.Value.ToString().NotNull())
        .ToDictionary(item => item.Value?.ToString().Trim(), item => item.End.Column - ExcelTool.EPPlusZero);
        return headers;
    }
}