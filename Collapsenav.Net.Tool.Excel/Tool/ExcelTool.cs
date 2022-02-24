using System.Data;
using NPOI.SS.UserModel;
using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel;

public partial class ExcelTool
{
    public const int EPPlusZero = 1;
    public const int NPOIZero = 0;
    public const int MiniZero = 0;

    /// <summary>
    /// 是否 xls 文件
    /// </summary>
    public static bool IsXls(string filepath)
    {
        if (filepath.IsEmpty())
            throw new NoNullAllowedException("文件路径不能为空");
        var ext = Path.GetExtension(filepath).ToLower();
        if (!ext.HasContain(".xlsx", ".xls"))
            throw new Exception("文件必须为excel格式");
        if (!File.Exists(filepath))
            throw new FileNotFoundException($@"我找不到这个叫 {filepath} 的文件,你看看是不是路径写错了");
        return ext == ".xls";
    }
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
        return sheetname.IsNull() ? sheets?[EPPlusZero] : sheets?[sheetname];
    }
    /// <summary>
    /// 获取 EPPlus中使用 的 Sheet
    /// </summary>
    public static ExcelWorksheet EPPlusSheet(Stream stream, string sheetname = null)
    {
        var sheets = EPPlusSheets(stream);
        return sheetname.IsNull() ? sheets?[EPPlusZero] : sheets?[sheetname];
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
        if (workbook.NumberOfSheets == 0)
            return null;
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
}