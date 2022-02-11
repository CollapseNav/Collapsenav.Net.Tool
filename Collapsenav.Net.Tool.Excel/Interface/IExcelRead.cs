using NPOI.SS.UserModel;
using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel;
/// <summary>
/// 尝试使用 IExcelRead 统一 NPOI , EPPlus , MiniExcel 的调用
/// </summary>
public interface IExcelRead : IExcelRead<string> { }
/// <summary>
/// 尝试使用 IExcelRead 统一 NPOI , EPPlus , MiniExcel 的调用
/// </summary>
public interface IExcelRead<T> : IExcelContainer<T>
{
#if NETSTANDARD2_0
#else
    public static IExcelRead GetExcelRead(object sheet)
    {
        if (sheet is ISheet)
        {
            return new NPOIExcelRead(sheet as ISheet);
        }
        else if (sheet is ExcelWorksheet)
        {
            return new EPPlusExcelRead(sheet as ExcelWorksheet);
        }
        return null;
    }
    /// <summary>
    /// 根据文件大小选择"合适"的 IExcelRead
    /// 5M以下随机选 EPPlus 或 NPOI
    /// 5M以上选 MiniExcel
    /// </summary>
    public static IExcelRead GetExcelRead(Stream stream, ExcelType? excelType = null)
    {
        excelType ??= stream.Length switch
        {
            >= 5 * 1024 * 1024 => ExcelType.MiniExcel,
            <= 5 * 1024 * 1024 => new Random().Next() % 2 == 0 ? ExcelType.EPPlus : ExcelType.NPOI,
        };
        IExcelRead read = excelType switch
        {
            ExcelType.EPPlus => new EPPlusExcelRead(stream),
            ExcelType.NPOI => new NPOIExcelRead(stream),
            ExcelType.MiniExcel => new MiniExcelRead(stream),
            _ => new MiniExcelRead(stream)
        };
        return read;
    }
    /// <summary>
    /// 根据文件大小选择"合适"的 IExcelRead
    /// 5M以下随机选 EPPlus 或 NPOI
    /// 5M以上选 MiniExcel
    /// </summary>
    public static IExcelRead GetExcelRead(string path, ExcelType? excelType = null)
    {
        using var fs = path.OpenReadShareStream();
        return GetExcelRead(fs, excelType);
    }
#endif
}