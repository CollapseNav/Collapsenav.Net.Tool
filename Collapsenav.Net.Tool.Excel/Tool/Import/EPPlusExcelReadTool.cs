using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel;
/// <summary>
/// 针对表格导入的一些操作(基于 EPPlus)
/// </summary>
public partial class EPPlusExcelReadTool
{
    private const int EPPlusZero = 1;

    #region 获取表头
    /// <summary>
    /// 获取表格header(仅限简单的单行表头)
    /// </summary>
    /// <param name="filepath">文件路径</param>
    public static IEnumerable<string> ExcelHeader(string filepath)
    {
        using var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
        return ExcelHeader(fs);
    }
    /// <summary>
    /// 获取表格header(仅限简单的单行表头)
    /// </summary>
    /// <param name="stream">文件流</param>
    public static IEnumerable<string> ExcelHeader(Stream stream)
    {
        using ExcelPackage pack = new(stream);
        return ExcelHeader(pack);
    }
    /// <summary>
    /// 获取表格header(仅限简单的单行表头)
    /// </summary>
    /// <param name="pack">excel workbook</param>
    public static IEnumerable<string> ExcelHeader(ExcelPackage pack)
    {
        var sheet = pack.Workbook.Worksheets[EPPlusZero];
        return ExcelHeader(sheet);
    }
    /// <summary>
    /// 获取表格header(仅限简单的单行表头)
    /// </summary>
    /// <param name="sheet">工作簿</param>
    public static IEnumerable<string> ExcelHeader(ExcelWorksheet sheet)
    {
        return ExcelReadTool.ExcelHeader(sheet);
    }
    #endregion


    #region 获取表格数据
    /// <summary>
    /// 获取表格的数据(仅限简单的单行表头)
    /// </summary>
    /// <param name="filepath">文件路径</param>
    public static async Task<IEnumerable<IEnumerable<string>>> ExcelDataAsync(string filepath)
    {
        using var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
        return await ExcelDataAsync(fs);
    }
    /// <summary>
    /// 获取表格的数据(仅限简单的单行表头)
    /// </summary>
    /// <param name="stream">文件流</param>
    public static async Task<IEnumerable<IEnumerable<string>>> ExcelDataAsync(Stream stream)
    {
        using ExcelPackage pack = new(stream);
        return await ExcelDataAsync(pack);
    }
    /// <summary>
    /// 获取表格的数据(仅限简单的单行表头)
    /// </summary>
    /// <param name="pack">excel workbook</param>
    public static async Task<IEnumerable<IEnumerable<string>>> ExcelDataAsync(ExcelPackage pack)
    {
        var sheet = pack.Workbook.Worksheets[EPPlusZero];
        return await ExcelDataAsync(sheet);
    }
    /// <summary>
    /// 获取表格的数据(仅限简单的单行表头)
    /// </summary>
    /// <param name="sheet">工作簿</param>
    public static async Task<IEnumerable<IEnumerable<string>>> ExcelDataAsync(ExcelWorksheet sheet)
    {
        return await ExcelReadTool.ExcelDataAsync(sheet);
    }
    /// <summary>
    /// 获取表格的数据(仅限简单的单行表头)
    /// </summary>
    /// <param name="sheet">工作簿</param>
    public static IEnumerable<IEnumerable<string>> ExcelData(ExcelWorksheet sheet)
    {
        return ExcelReadTool.ExcelData(sheet);
    }
    #endregion


    #region 通过表格配置获取表头
    /// <summary>
    /// 获取表头及其index
    /// </summary>
    /// <param name="filepath">文件路径</param>
    /// <param name="options">导出配置</param>
    public static Dictionary<string, int> ExcelHeaderByOptions<T>(string filepath, ReadConfig<T> options)
    {
        using var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
        return ExcelHeaderByOptions(fs, options);
    }
    /// <summary>
    /// 获取表头及其index
    /// </summary>
    /// <param name="stream">文件流</param>
    /// <param name="options">导出配置</param>
    public static Dictionary<string, int> ExcelHeaderByOptions<T>(Stream stream, ReadConfig<T> options)
    {
        using ExcelPackage pack = new(stream);
        return ExcelHeaderByOptions(pack, options);
    }
    /// <summary>
    /// 获取表头及其index
    /// </summary>
    /// <param name="pack">excel workbook</param>
    /// <param name="options">导出配置</param>
    public static Dictionary<string, int> ExcelHeaderByOptions<T>(ExcelPackage pack, ReadConfig<T> options)
    {
        var sheet = pack.Workbook.Worksheets[EPPlusZero];
        return ExcelHeaderByOptions(sheet, options);
    }
    /// <summary>
    /// 获取表头及其index
    /// </summary>
    /// <param name="sheet">工作簿</param>
    /// <param name="options">导出配置</param>
    public static Dictionary<string, int> ExcelHeaderByOptions<T>(ExcelWorksheet sheet, ReadConfig<T> options)
    {
        return ExcelReadTool.ExcelHeaderByOptions(sheet, options);
    }
    #endregion


    #region 通过表格配置获取表格数据
    /// <summary>
    /// 根据配置 获取表格数据
    /// </summary>
    /// <param name="filepath">文件路径</param>
    /// <param name="options">导出配置</param>
    public static async Task<string[][]> ExcelDataByOptionsAsync<T>(string filepath, ReadConfig<T> options)
    {
        using var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
        return await ExcelDataByOptionsAsync(fs, options);
    }
    /// <summary>
    /// 根据配置 获取表格数据
    /// </summary>
    /// <param name="stream">文件流</param>
    /// <param name="options">导出配置</param>
    public static async Task<string[][]> ExcelDataByOptionsAsync<T>(Stream stream, ReadConfig<T> options)
    {
        using ExcelPackage pack = new(stream);
        return await ExcelDataByOptionsAsync(pack, options);
    }
    /// <summary>
    /// 根据配置 获取表格数据
    /// </summary>
    /// <param name="pack">excel workbook</param>
    /// <param name="options">导出配置</param>
    public static async Task<string[][]> ExcelDataByOptionsAsync<T>(ExcelPackage pack, ReadConfig<T> options)
    {
        var sheet = pack.Workbook.Worksheets[EPPlusZero];
        return await ExcelDataByOptionsAsync(sheet, options);
    }
    /// <summary>
    /// 根据配置 获取表格数据
    /// </summary>
    /// <param name="sheet">工作簿</param>
    /// <param name="options">导出配置</param>
    public static async Task<string[][]> ExcelDataByOptionsAsync<T>(ExcelWorksheet sheet, ReadConfig<T> options)
    {
        return await ExcelReadTool.ExcelDataByOptionsAsync(sheet, options);
    }
    #endregion


    #region 将表格数据转换为指定的数据实体
    /// <summary>
    /// 将表格数据转换为指定的数据实体
    /// </summary>
    /// <param name="filepath">文件路径</param>
    /// <param name="options">导出配置</param>
    public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(string filepath, ReadConfig<T> options)
    {
        using var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
        return await ExcelToEntityAsync(fs, options);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体
    /// </summary>
    /// <param name="stream">文件流</param>
    /// <param name="options">导出配置</param>
    public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(Stream stream, ReadConfig<T> options)
    {
        using ExcelPackage pack = new(stream);
        return await ExcelToEntityAsync(pack, options);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体
    /// </summary>
    /// <param name="pack">excel workbook</param>
    /// <param name="options">导出配置</param>
    public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(ExcelPackage pack, ReadConfig<T> options)
    {
        var sheet = pack.Workbook.Worksheets[EPPlusZero];
        return await ExcelToEntityAsync(sheet, options);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体
    /// </summary>
    /// <param name="sheet">工作簿</param>
    /// <param name="options">导出配置</param>
    public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(ExcelWorksheet sheet, ReadConfig<T> options)
    {
        // 合并 FieldOption 和 DefaultOption
        var header = ExcelHeaderByOptions<T>(sheet, options);
        var excelData = await ExcelDataByOptionsAsync<T>(sheet, options);
        return await ExcelReadTool.ExcelToEntityAsync(header, excelData, options);
    }
    #endregion
}
