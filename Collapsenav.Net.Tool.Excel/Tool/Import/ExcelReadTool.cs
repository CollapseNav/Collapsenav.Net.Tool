using System.Collections.Concurrent;
using NPOI.SS.UserModel;
using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel;
public class ExcelReadTool
{
    private const int EPPlusZero = 1;
    private const int NPOIZero = 0;

    /// <summary>
    /// 将表格数据转换为T类型的集合
    /// </summary>
    public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(IExcelRead sheet, ReadConfig<T> options)
    {
        ConcurrentBag<T> data = new();
        var header = sheet.HeadersWithIndex;
        var rowCount = sheet.RowCount;
        await Task.Factory.StartNew(() =>
        {
            Parallel.For(1, rowCount, index =>
            {
                Monitor.Enter(sheet);
                var dataRow = sheet[index].ToList();
                Monitor.Exit(sheet);
                // 根据对应传入的设置 为obj赋值
                var obj = Activator.CreateInstance<T>();
                foreach (var option in options.FieldOption)
                {
                    if (!option.ExcelField.IsNull())
                    {
                        var value = dataRow[header[option.ExcelField]];
                        option.Prop.SetValue(obj, option.Action == null ? value : option.Action(value));
                    }
                    else
                        option.Prop.SetValue(obj, option.Action == null ? null : option.Action(string.Empty));
                }
                options.Init?.Invoke(obj);
                data.Add(obj);
            });
        });
        return data;
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
    /// 获取表头及其index
    /// </summary>
    /// <param name="sheet">工作簿</param>
    /// <param name="options">导出配置</param>
    public static Dictionary<string, int> ExcelHeaderByOptions<T>(ExcelWorksheet sheet, ReadConfig<T> options)
    {
        // 获取对应设置的 表头 以及其 column
        var header = sheet.Cells[EPPlusZero, EPPlusZero, EPPlusZero, sheet.Dimension.Columns]
        .Where(item => options.FieldOption.Any(opt => opt.ExcelField == item.Value?.ToString().Trim()))
        .ToDictionary(item => item.Value?.ToString().Trim(), item => item.End.Column - EPPlusZero);
        return header;
    }

    /// <summary>
    /// 获取表格header(仅限简单的单行表头)
    /// </summary>
    /// <param name="sheet">工作簿</param>
    public static IEnumerable<string> ExcelHeader(ISheet sheet)
    {
        var header = sheet.GetRow(NPOIZero).Cells.Select(item => item.ToString()?.Trim());
        return header;
    }
    /// <summary>
    /// 根据传入配置 获取表头及其index
    /// </summary>
    /// <param name="sheet">工作簿</param>
    /// <param name="options">导出配置</param>
    public static Dictionary<string, int> ExcelHeaderByOptions<T>(ISheet sheet, ReadConfig<T> options)
    {
        // 获取对应设置的 表头 以及其 column
        var header = sheet.GetRow(NPOIZero).Cells
        .Where(item => options.FieldOption.Any(opt => opt.ExcelField == item.ToString()?.Trim()))
        .ToDictionary(item => item.ToString()?.Trim(), item => item.ColumnIndex);
        return header;
    }
    /// <summary>
    /// 是否 xls 文件
    /// </summary>
    public static bool IsXls(string filepath)
    {
        if (!File.Exists(filepath))
            throw new FileNotFoundException($@"我找不到这个叫 {filepath} 的文件,你看看是不是路径写错了");
        var ext = Path.GetExtension(filepath).ToLower();
        if (!new[] { ".xlsx", ".xls" }.Contains(ext))
            throw new Exception("文件必须为excel格式");
        return ext == ".xls";
    }
    /// <summary>
    /// 获取 EPPlus中使用 的 ExcelPackage
    /// </summary>
    public static ExcelPackage GetEPPlusPackage(Stream stream)
    {
        ExcelPackage pack = new(stream);
        return pack;
    }
    public static ExcelPackage GetEPPlusPackage(string path)
    {
        using var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        ExcelPackage pack = new(fs);
        return pack;
    }
    /// <summary>
    /// 获取 EPPlus中使用 的 Workbook
    /// </summary>
    public static ExcelWorkbook GetEPPlusWorkbook(string path)
    {
        return GetEPPlusPackage(path)?.Workbook;
    }
    /// <summary>
    /// 获取 EPPlus中使用 的 Workbook
    /// </summary>
    public static ExcelWorkbook GetEPPlusWorkbook(Stream stream)
    {
        return GetEPPlusPackage(stream)?.Workbook;
    }
    /// <summary>
    /// 获取 EPPlus中使用 的 Sheets
    /// </summary>
    public static ExcelWorksheets GetEPPlusSheets(string path)
    {
        return GetEPPlusWorkbook(path).Worksheets;
    }
    /// <summary>
    /// 获取 EPPlus中使用 的 Sheets
    /// </summary>
    public static ExcelWorksheets GetEPPlusSheets(Stream stream)
    {
        return GetEPPlusWorkbook(stream).Worksheets;
    }
    /// <summary>
    /// 获取 EPPlus中使用 的 Sheet
    /// </summary>
    public static ExcelWorksheet GetEPPlusSheet(string path, string sheetname = null)
    {
        return sheetname.IsNull() ? GetEPPlusSheets(path)[EPPlusZero] : GetEPPlusSheets(path)[sheetname];
    }
    /// <summary>
    /// 获取 EPPlus中使用 的 Sheet
    /// </summary>
    public static ExcelWorksheet GetEPPlusSheet(Stream stream, string sheetname = null)
    {
        return sheetname.IsNull() ? GetEPPlusSheets(stream)[EPPlusZero] : GetEPPlusSheets(stream)[sheetname];
    }
    /// <summary>
    /// 获取 EPPlus中使用 的 Sheet
    /// </summary>
    public static ExcelWorksheet GetEPPlusSheet(string path, int sheetindex)
    {
        return GetEPPlusWorkbook(path).Worksheets[sheetindex];
    }
    /// <summary>
    /// 获取 EPPlus中使用 的 Sheet
    /// </summary>
    public static ExcelWorksheet GetEPPlusSheet(Stream stream, int sheetindex)
    {
        return GetEPPlusWorkbook(stream).Worksheets[sheetindex];
    }
    /// <summary>
    /// 获取 NPOI中使用 的 Workbook
    /// </summary>
    public static IWorkbook GetNPOIWorkbook(string path)
    {
        using var notCloseStream = new NPOINotCloseStream(path);
        return notCloseStream.GetWorkBook();
    }
    /// <summary>
    /// 获取 NPOI中使用 的 Workbook
    /// </summary>
    public static IWorkbook GetNPOIWorkbook(Stream stream)
    {
        using var notCloseStream = new NPOINotCloseStream(stream);
        return notCloseStream.GetWorkBook();
    }
    /// <summary>
    /// 获取 NPOI中使用 的 Sheet
    /// </summary>
    public static ISheet GetNPOISheet(string path, string sheetname = null)
    {
        var workbook = GetNPOIWorkbook(path);
        return sheetname.IsNull() ? workbook.GetSheetAt(NPOIZero) : workbook.GetSheet(sheetname);
    }
    /// <summary>
    /// 获取 NPOI中使用 的 Sheet
    /// </summary>
    public static ISheet GetNPOISheet(Stream stream, string sheetname = null)
    {
        var workbook = GetNPOIWorkbook(stream);
        return sheetname.IsNull() ? workbook.GetSheetAt(NPOIZero) : workbook.GetSheet(sheetname);
    }
    /// <summary>
    /// 获取 NPOI中使用 的 Sheet
    /// </summary>
    public static ISheet GetNPOISheet(string path, int sheetindex)
    {
        var workbook = GetNPOIWorkbook(path);
        return workbook.GetSheetAt(sheetindex);
    }
    /// <summary>
    /// 获取 NPOI中使用 的 Sheet
    /// </summary>
    public static ISheet GetNPOISheet(Stream stream, int sheetindex)
    {
        var workbook = GetNPOIWorkbook(stream);
        return workbook.GetSheetAt(sheetindex);
    }
    /// <summary>
    /// 获取表格header和对应的index
    /// </summary>
    public static IDictionary<string, int> HeadersWithIndex(ISheet sheet)
    {
        var headers = sheet.GetRow(NPOIZero).Cells
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
