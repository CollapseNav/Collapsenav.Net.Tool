namespace Collapsenav.Net.Tool.Excel
{
    public static class ExcelExt
    {
        public static bool IsXls(this string filepath)
        {
            return ExcelTool.IsXls(filepath);
        }
    }
}
