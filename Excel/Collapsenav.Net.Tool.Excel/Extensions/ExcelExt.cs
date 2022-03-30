namespace Collapsenav.Net.Tool.Excel;
public static class ExcelExt
{
    /// <summary>
    /// 是否 xls 文件
    /// </summary>
    public static bool IsXls(this string filepath)
    {
        return ExcelTool.IsXls(filepath);
    }
}
