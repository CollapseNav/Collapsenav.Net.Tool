using NPOI.SS.UserModel;
using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel;
public class ExcelTool
{
    public const int EPPlusZero = 1;
    public const int NPOIZero = 0;
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
    /// 获取 EPPlus中使用 的 Sheets
    /// </summary>
    public static ExcelWorksheets GetEPPlusSheets(string path)
    {
        return GetEPPlusWorkbook(path).Worksheets;
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
    public static ExcelWorksheet GetEPPlusSheet(string path, int sheetindex = EPPlusZero)
    {
        return GetEPPlusWorkbook(path).Worksheets[sheetindex];
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
    public static ISheet GetNPOISheet(string path, int sheetindex = NPOIZero)
    {
        var workbook = GetNPOIWorkbook(path);
        return workbook.GetSheetAt(sheetindex);
    }
}
