namespace Collapsenav.Net.Tool.Excel;
/// <summary>
/// 尝试使用 IExcelRead 统一 NPOI , EPPlus , MiniExcel 的调用
/// </summary>
public interface IExcelReader : IExcelReader<string> { }
/// <summary>
/// 尝试使用 IExcelRead 统一 NPOI , EPPlus , MiniExcel 的调用
/// </summary>
public interface IExcelReader<T> : IExcelContainer<T>
{
#if NET6_0_OR_GREATER && NETCOREAPP
    public static IExcelReader GetExcelReader(object sheet)
    {
        return ExcelReaderSelector.GetExcelReader(sheet);
    }
    public static IExcelReader GetExcelReader(Stream stream, ExcelType? excelType = null)
    {
        return ExcelReaderSelector.GetExcelReader(stream, excelType.ToString());
    }
    public static IExcelReader GetExcelReader(string path, ExcelType? excelType = null)
    {
        using var fs = path.OpenReadWriteShareStream();
        return GetExcelReader(fs, excelType);
    }
#endif
}