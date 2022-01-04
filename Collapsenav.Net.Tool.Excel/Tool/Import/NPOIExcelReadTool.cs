using NPOI.SS.UserModel;

namespace Collapsenav.Net.Tool.Excel;
/// <summary>
/// 针对表格导入的一些操作(基于 NPOI)
/// </summary>
public partial class NPOIExcelReadTool
{
    private const int NPOIZero = 0;

    #region 获取表头
    /// <summary>
    /// 获取表格header(仅限简单的单行表头)
    /// </summary>
    /// <param name="filepath">文件路径</param>
    public static IEnumerable<string> ExcelHeader(string filepath)
    {
        filepath.IsXls();
        using var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
        return ExcelHeader(fs);
    }
    /// <summary>
    /// 获取表格header(仅限简单的单行表头)
    /// </summary>
    /// <param name="stream">文件流</param>
    public static IEnumerable<string> ExcelHeader(Stream stream)
    {
        using var notCloseStream = new NPOINotCloseStream(stream);
        return ExcelHeader(notCloseStream.GetWorkBook());
    }
    /// <summary>
    /// 获取表格header(仅限简单的单行表头)
    /// </summary>
    /// <param name="workbook">excel workbook</param>
    public static IEnumerable<string> ExcelHeader(IWorkbook workbook)
    {
        var sheet = workbook.GetSheetAt(NPOIZero);
        return ExcelHeader(sheet);
    }
    /// <summary>
    /// 获取表格header(仅限简单的单行表头)
    /// </summary>
    /// <param name="sheet">工作簿</param>
    public static IEnumerable<string> ExcelHeader(ISheet sheet)
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
        filepath.IsXls();
        using var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
        return await ExcelDataAsync(fs);
    }
    /// <summary>
    /// 获取表格的数据(仅限简单的单行表头)
    /// </summary>
    /// <param name="stream">文件流</param>
    public static async Task<IEnumerable<IEnumerable<string>>> ExcelDataAsync(Stream stream)
    {
        using var notCloseStream = new NPOINotCloseStream(stream);
        return await ExcelDataAsync(notCloseStream.GetWorkBook());
    }
    /// <summary>
    /// 获取表格的数据(仅限简单的单行表头)
    /// </summary>
    /// <param name="workbook">excel workbook</param>
    public static async Task<IEnumerable<IEnumerable<string>>> ExcelDataAsync(IWorkbook workbook)
    {
        var sheet = workbook.GetSheetAt(NPOIZero);
        return await ExcelDataAsync(sheet);
    }
    /// <summary>
    /// 获取表格的数据(仅限简单的单行表头)
    /// </summary>
    /// <param name="sheet">工作簿</param>
    public static async Task<IEnumerable<IEnumerable<string>>> ExcelDataAsync(ISheet sheet)
    {
        return await ExcelReadTool.ExcelDataAsync(sheet);
    }
    #endregion


    #region 通过表格配置获取表头
    /// <summary>
    /// 根据传入配置 获取表头及其index
    /// </summary>
    /// <param name="filepath">文件路径</param>
    /// <param name="options">导出配置</param>
    public static Dictionary<string, int> ExcelHeaderByOptions<T>(string filepath, ReadConfig<T> options)
    {
        filepath.IsXls();
        using var fs = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.Read);
        return ExcelHeaderByOptions(fs, options);
    }
    /// <summary>
    /// 根据传入配置 获取表头及其index
    /// </summary>
    /// <param name="stream">文件流</param>
    /// <param name="options">导出配置</param>
    public static Dictionary<string, int> ExcelHeaderByOptions<T>(Stream stream, ReadConfig<T> options)
    {
        using var notCloseStream = new NPOINotCloseStream(stream);
        return ExcelHeaderByOptions(notCloseStream.GetWorkBook(), options);
    }
    /// <summary>
    /// 根据传入配置 获取表头及其index
    /// </summary>
    /// <param name="workbook">excel workbook</param>
    /// <param name="options">导出配置</param>
    public static Dictionary<string, int> ExcelHeaderByOptions<T>(IWorkbook workbook, ReadConfig<T> options)
    {
        var sheet = workbook.GetSheetAt(NPOIZero);
        return ExcelHeaderByOptions(sheet, options);
    }
    /// <summary>
    /// 根据传入配置 获取表头及其index
    /// </summary>
    /// <param name="sheet">工作簿</param>
    /// <param name="options">导出配置</param>
    public static Dictionary<string, int> ExcelHeaderByOptions<T>(ISheet sheet, ReadConfig<T> options)
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
        filepath.IsXls();
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
        using var notCloseStream = new NPOINotCloseStream(stream);
        return await ExcelDataByOptionsAsync(notCloseStream.GetWorkBook(), options);
    }
    /// <summary>
    /// 根据配置 获取表格数据
    /// </summary>
    /// <param name="workbook">excel workbook</param>
    /// <param name="options">导出配置</param>
    public static async Task<string[][]> ExcelDataByOptionsAsync<T>(IWorkbook workbook, ReadConfig<T> options)
    {
        var sheet = workbook.GetSheetAt(NPOIZero);
        return await ExcelDataByOptionsAsync(sheet, options);
    }
    /// <summary>
    /// 根据配置 获取表格数据
    /// </summary>
    /// <param name="sheet">工作簿</param>
    /// <param name="options">导出配置</param>
    public static async Task<string[][]> ExcelDataByOptionsAsync<T>(ISheet sheet, ReadConfig<T> options)
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
        filepath.IsXls();
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
        using var notCloseStream = new NPOINotCloseStream(stream);
        return await ExcelToEntityAsync(notCloseStream.GetWorkBook(), options);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体
    /// </summary>
    /// <param name="workbook">excel workbook</param>
    /// <param name="options">导出配置</param>
    public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(IWorkbook workbook, ReadConfig<T> options)
    {
        var sheet = workbook.GetSheetAt(NPOIZero);
        return await ExcelToEntityAsync(sheet, options);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体
    /// </summary>
    /// <param name="sheet">工作簿</param>
    /// <param name="options">导出配置</param>
    public static async Task<IEnumerable<T>> ExcelToEntityAsync<T>(ISheet sheet, ReadConfig<T> options)
    {
        // 合并 FieldOption 和 DefaultOption
        var header = ExcelHeaderByOptions<T>(sheet, options);
        var excelData = await ExcelDataByOptionsAsync(sheet, options);
        return await ExcelReadTool.ExcelToEntityAsync(header, excelData, options);
    }
    #endregion
}
