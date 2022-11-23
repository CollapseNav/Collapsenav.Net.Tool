using System.Data;

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

    public static IExcelCellReader GetCellReader(string path, ExcelType? excelType = null)
    {
        using var fs = path.OpenReadShareStream();
        return CellReaderSelector.GetCellReader(fs, excelType.ToString());
    }
    public static IExcelCellReader GetCellReader(Stream stream, ExcelType? excelType = null)
    {
        return CellReaderSelector.GetCellReader(stream, excelType.ToString());
    }
    public static IExcelCellReader GetCellReader(object obj, ExcelType? excelType = null)
    {
        return CellReaderSelector.GetCellReader(obj, excelType.ToString());
    }


    public static IExcelReader GetExcelReader(string path, ExcelType? excelType = null)
    {
        using var fs = path.OpenReadShareStream();
        return ExcelReaderSelector.GetExcelReader(fs, excelType.ToString());
    }
    public static IExcelReader GetExcelReader(Stream stream, ExcelType? excelType = null)
    {
        return ExcelReaderSelector.GetExcelReader(stream, excelType.ToString());
    }
    public static IExcelReader GetExcelReader(object obj, ExcelType? excelType = null)
    {
        return ExcelReaderSelector.GetExcelReader(obj, excelType.ToString());
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
        var reader = GetExcelReader(stream);
        return await ExcelToEntityAsync(reader, config);
    }
    /// <summary>
    /// 将表格数据转换为T类型的集合
    /// </summary>
    public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(IExcelReader reader, ReadConfig<T> config = null)
    {
        config ??= ReadConfig<T>.GenDefaultConfig();
        return await config.ToEntityAsync(reader);
    }
    /// <summary>
    /// 将表格数据转换为T类型的集合(更快)
    /// </summary>
    public static IEnumerable<T> ExcelToEntity<T>(string path, ReadConfig<T> config = null)
    {
        using var fs = path.OpenReadShareStream();
        return ExcelToEntity(fs, config);
    }
    /// <summary>
    /// 将表格数据转换为T类型的集合(更快)
    /// </summary>
    public static IEnumerable<T> ExcelToEntity<T>(Stream stream, ReadConfig<T> config = null)
    {
        var reader = GetExcelReader(stream);
        return ExcelToEntity(reader, config);
    }
    /// <summary>
    /// 将表格数据转换为T类型的集合(更快)
    /// </summary>
    public static IEnumerable<T> ExcelToEntity<T>(IExcelReader reader, ReadConfig<T> config = null)
    {
        config ??= ReadConfig<T>.GenDefaultConfig();
        return config.ToEntity(reader);
    }

    /// <summary>
    /// 配置导出表头为Excel
    /// </summary>
    public static Stream ExportHeader<T>(string path)
    {
        var config = ExportConfig<T>.GenDefaultConfig();
        return config.ExportHeader(path);
    }
    /// <summary>
    /// 配置导出表头为Excel
    /// </summary>
    public static Stream ExportHeader<T>(Stream stream)
    {
        var config = ExportConfig<T>.GenDefaultConfig();
        return config.ExportHeader(stream);
    }
    /// <summary>
    /// 导出excel
    /// </summary>
    public static async Task<Stream> ExportAsync<T>(string path, IEnumerable<T> data = null)
    {
        var config = ExportConfig<T>.GenDefaultConfig(data);
        return await config.ExportAsync(path, data);
    }
    /// <summary>
    /// 导出excel
    /// </summary>
    public static async Task<Stream> ExportAsync<T>(Stream stream, IEnumerable<T> data = null)
    {
        var config = ExportConfig<T>.GenDefaultConfig(data);
        return await config.ExportAsync(stream, data);
    }
    /// <summary>
    /// 导出excel
    /// </summary>
    public static async Task<Stream> ExportAsync<T>(Stream stream, ExportConfig<T> config, IEnumerable<T> data = null)
    {
        return await config.ExportAsync(stream, data);
    }
    /// <summary>
    /// 导出excel
    /// </summary>
    public static async Task<Stream> ExportAsync<T>(string path, ExportConfig<T> config, IEnumerable<T> data = null)
    {
        return await config.ExportAsync(path, data);
    }
}