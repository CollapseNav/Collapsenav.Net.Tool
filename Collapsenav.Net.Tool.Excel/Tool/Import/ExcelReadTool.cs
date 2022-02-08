using System.Data;
using NPOI.SS.UserModel;
using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel;
public partial class ExcelTool
{

    /// <summary>
    /// 将表格数据转换为T类型的集合
    /// </summary>
    public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(string path, ReadConfig<T> config = null)
    {
        using var fs = path.OpenReadShareStream();
        return await ExcelToEntityAsync(fs, config);
    }
    /// <summary>
    /// 将表格数据转换为T类型的集合
    /// </summary>
    public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(Stream stream, ReadConfig<T> config = null)
    {
        var reader = IExcelRead.GetExcelRead(stream);
        return await ExcelToEntityAsync(reader, config);
    }
    /// <summary>
    /// 将表格数据转换为T类型的集合
    /// </summary>
    public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(IExcelRead reader, ReadConfig<T> config = null)
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
        return sheet.Cells[EPPlusZero, EPPlusZero, EPPlusZero, sheet.Dimension.Columns]
                .Select(item => item.Value?.ToString().Trim()).ToList();
    }
    /// <summary>
    /// 获取表格header(仅限简单的单行表头)
    /// </summary>
    /// <param name="sheet">工作簿</param>
    public static IEnumerable<string> ExcelHeader(ISheet sheet)
    {
        var header = sheet.GetRow(NPOIZero)?.Cells.Select(item => item.ToString()?.Trim());
        return header;
    }
    /// <summary>
    /// 获取表格header和对应的index
    /// </summary>
    public static IDictionary<string, int> HeadersWithIndex(ISheet sheet)
    {
        var headers = sheet.GetRow(NPOIZero)?.Cells
        .Where(item => item.ToString().NotNull())
        .ToDictionary(item => item.ToString()?.Trim(), item => item.ColumnIndex);
        return headers;
    }
    /// <summary>
    /// 获取表格header和对应的index
    /// </summary>
    public static IDictionary<string, int> HeadersWithIndex(ExcelWorksheet sheet)
    {
        var headers = sheet.Cells[EPPlusZero, EPPlusZero, EPPlusZero, sheet.Dimension.Columns]
        .Where(item => item.Value.ToString().NotNull())
        .ToDictionary(item => item.Value?.ToString().Trim(), item => item.End.Column - EPPlusZero);
        return headers;
    }
}