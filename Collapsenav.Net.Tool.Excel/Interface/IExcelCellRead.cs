using NPOI.SS.UserModel;
using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel;
/// <summary>
/// 尝试使用 IExcelRead 统一 NPOI , EPPlus , MiniExcel 的调用
/// </summary>
public interface IExcelCellRead : IExcelContainer<IReadCell>
{
    void Save();
    void SaveTo(Stream stream);
    void SaveTo(string path);
    Stream GetStream();

#if NETSTANDARD2_0
#else
    public static IExcelCellRead GetCellRead(object sheet)
    {
        if (sheet is ISheet)
        {
            return new NPOICellRead(sheet as ISheet);
        }
        else if (sheet is ExcelWorksheet)
        {
            return new EPPlusCellRead(sheet as ExcelWorksheet);
        }
        return null;
    }
    /// <summary>
    /// 根据文件大小选择"合适"的 IExcelRead
    /// 5M以下随机选 EPPlus 或 NPOI
    /// 5M以上选 MiniExcel
    /// </summary>
    public static IExcelCellRead GetCellRead(Stream stream, ExcelType? excelType = null)
    {
        excelType ??= stream.Length switch
        {
            >= 5 * 1024 * 1024 => ExcelType.MiniExcel,
            <= 5 * 1024 * 1024 => new Random().Next() % 2 == 0 ? ExcelType.EPPlus : ExcelType.NPOI,
        };
        IExcelCellRead read = excelType switch
        {
            ExcelType.EPPlus => new EPPlusCellRead(stream),
            ExcelType.NPOI => new NPOICellRead(stream),
            ExcelType.MiniExcel => new MiniCellRead(stream),
            _ => new MiniCellRead(stream)
        };
        return read;
    }
    /// <summary>
    /// 根据文件大小选择"合适"的 IExcelRead
    /// 5M以下随机选 EPPlus 或 NPOI
    /// 5M以上选 MiniExcel
    /// </summary>
    public static IExcelCellRead GetCellRead(string path, ExcelType? excelType = null)
    {
        var fs = path.OpenCreateReadWriteShareStream();
        return GetCellRead(fs, excelType);
    }
#endif
}
