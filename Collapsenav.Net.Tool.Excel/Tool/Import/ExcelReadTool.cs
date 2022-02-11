using System.Data;
using NPOI.SS.UserModel;
using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel;
public partial class ExcelTool
{
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


    /// <summary>
    /// 将表格数据转换为T类型的集合
    /// </summary>
    public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(string path, ReadConfig<T> config = null)
    {
        using var fs = path.OpenReadShareStream();
        return await ExcelToEntityAsync(fs, config);
    }
    /// <summary>
    /// 将表格数据转换为T类型的集合
    /// </summary>
    public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(Stream stream, ReadConfig<T> config = null)
    {
        var reader = GetExcelRead(stream);
        return await ExcelToEntityAsync(reader, config);
    }
    /// <summary>
    /// 将表格数据转换为T类型的集合
    /// </summary>
    public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(IExcelRead reader, ReadConfig<T> config = null)
    {
        config ??= ReadConfig<T>.GenDefaultConfig();
        return await config.ToEntityAsync(reader);
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
    /// 获取表格header(仅限简单的单行表头)
    /// </summary>
    /// <param name="sheet">工作簿</param>
    public static IEnumerable<string> ExcelHeader(ISheet sheet)
    {
        var header = sheet.GetRow(NPOIZero)?.Cells.Select(item => item.ToString()?.Trim());
        return header;
    }
    /// <summary>
    /// 获取表格header和对应的index
    /// </summary>
    public static IDictionary<string, int> HeadersWithIndex(ISheet sheet)
    {
        var headers = sheet.GetRow(NPOIZero)?.Cells
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