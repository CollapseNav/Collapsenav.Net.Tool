using System.Collections.Concurrent;
using System.Data;
using NPOI.SS.UserModel;
using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel;
public class ExcelReadTool
{
    public const int EPPlusZero = 1;
    public const int NPOIZero = 0;

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
    /// 将表格数据转换为T类型的集合
    /// </summary>
    public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(string path)
    {
        return await ReadConfig<T>.ExcelToEntityAsync(path);
    }
    /// <summary>
    /// 将表格数据转换为T类型的集合
    /// </summary>
    public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(Stream stream)
    {
        return await ReadConfig<T>.ExcelToEntityAsync(stream);
    }
    /// <summary>
    /// 将表格数据转换为T类型的集合
    /// </summary>
    public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(IExcelRead reader)
    {
        return await ReadConfig<T>.ExcelToEntityAsync(reader);
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
        if (filepath.IsEmpty())
            throw new NoNullAllowedException("文件路径不能为空");
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
    public static ExcelPackage EPPlusPackage(Stream stream)
    {
        ExcelPackage pack = new(stream);
        return pack;
    }
    public static ExcelPackage EPPlusPackage(string path)
    {
        using var fs = path.ReadShareStream();
        ExcelPackage pack = new(fs);
        return pack;
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
        return EPPlusWorkbook(path).Worksheets;
    }
    /// <summary>
    /// 获取 EPPlus中使用 的 Sheets
    /// </summary>
    public static ExcelWorksheets EPPlusSheets(Stream stream)
    {
        return EPPlusWorkbook(stream).Worksheets;
    }
    /// <summary>
    /// 获取 EPPlus中使用 的 Sheet
    /// </summary>
    public static ExcelWorksheet EPPlusSheet(string path, string sheetname = null)
    {
        return sheetname.IsNull() ? EPPlusSheets(path)[EPPlusZero] : EPPlusSheets(path)[sheetname];
    }
    /// <summary>
    /// 获取 EPPlus中使用 的 Sheet
    /// </summary>
    public static ExcelWorksheet EPPlusSheet(Stream stream, string sheetname = null)
    {
        return sheetname.IsNull() ? EPPlusSheets(stream)[EPPlusZero] : EPPlusSheets(stream)[sheetname];
    }
    /// <summary>
    /// 获取 EPPlus中使用 的 Sheet
    /// </summary>
    public static ExcelWorksheet EPPlusSheet(string path, int sheetindex)
    {
        return EPPlusWorkbook(path).Worksheets[sheetindex];
    }
    /// <summary>
    /// 获取 EPPlus中使用 的 Sheet
    /// </summary>
    public static ExcelWorksheet EPPlusSheet(Stream stream, int sheetindex)
    {
        return EPPlusWorkbook(stream).Worksheets[sheetindex];
    }
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
        using var notCloseStream = new NPOINotCloseStream(stream);
        return notCloseStream.GetWorkBook();
    }
    /// <summary>
    /// 获取 NPOI中使用 的 Sheet
    /// </summary>
    public static ISheet NPOISheet(string path, string sheetname = null)
    {
        var workbook = NPOIWorkbook(path);
        return sheetname.IsNull() ? workbook.GetSheetAt(NPOIZero) : workbook.GetSheet(sheetname);
    }
    /// <summary>
    /// 获取 NPOI中使用 的 Sheet
    /// </summary>
    public static ISheet NPOISheet(Stream stream, string sheetname = null)
    {
        var workbook = NPOIWorkbook(stream);
        return sheetname.IsNull() ? workbook.GetSheetAt(NPOIZero) : workbook.GetSheet(sheetname);
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
