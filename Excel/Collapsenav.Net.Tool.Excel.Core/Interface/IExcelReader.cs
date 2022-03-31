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
#if NETSTANDARD2_0
#else
    public static IExcelReader GetExcelRead(object sheet)
    {
        return ExcelReaderSelector.GetExcelReader(sheet);
    }
    public static IExcelReader GetExcelRead(Stream stream, ExcelType? excelType = null)
    {
        return ExcelReaderSelector.GetExcelReader(stream, excelType);
    }
    public static IExcelReader GetExcelRead(string path, ExcelType? excelType = null)
    {
        using var fs = path.OpenCreateReadWriteShareStream();
        return GetExcelRead(fs, excelType);
    }
#endif
}