using NPOI.XSSF.Streaming;
using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel;

public class ExcelExportTool
{
    public const int EPPlusZero = 1;
    public const int NPOIZero = 0;

    /// <summary>
    /// 数据实体导出为Excel
    /// </summary>
    public static async Task<Stream> DataExportAsync<T>(string path, IEnumerable<T> data)
    {
        return await ExportConfig<T>.DataExportAsync(path, data);
    }
    /// <summary>
    /// 数据实体导出为Excel
    /// </summary>
    public static async Task<Stream> DataExportAsync<T>(Stream stream, IEnumerable<T> data)
    {
        return await ExportConfig<T>.DataExportAsync(stream, data);
    }

    /// <summary>
    /// 配置导出表头为Excel
    /// </summary>
    public static Stream ConfigExportHeader<T>(string path)
    {
        return ExportConfig<T>.ConfigExportHeader(path);
    }
    /// <summary>
    /// 配置导出表头为Excel
    /// </summary>
    public static Stream ConfigExportHeader<T>(Stream stream)
    {
        return ExportConfig<T>.ConfigExportHeader(stream);
    }
    /// <summary>
    /// 导出excel
    /// </summary>
    /// <param name="excel">excel</param>
    /// <param name="option">导出配置</param>
    /// <param name="data">指定数据</param>
    public static async Task<Stream> ExportAsync<T>(IExcelCellRead excel, ExportConfig<T> option, IEnumerable<T> data = null)
    {
        Monitor.Enter(excel);
        await Task.Factory.StartNew(() =>
        {
            foreach (var (cellData, rowIndex) in (data ?? option.Data).SelectWithIndex())
                foreach (var (item, index) in option.FieldOption.Select((item, i) => (item, i)))
                    excel[rowIndex, index].Value = item.Action(cellData);
        });
        Monitor.Exit(excel);
        return excel.GetStream();
    }
    /// <summary>
    /// 导出excel
    /// </summary>
    public static async Task<Stream> ExportAsync<T>(string path, IEnumerable<T> data = null)
    {
        var config = ExportConfig<T>.GenDefaultConfig(data);
        return await ExportAsync(path, config, data);
    }
    /// <summary>
    /// 导出excel
    /// </summary>
    public static async Task<Stream> ExportAsync<T>(Stream stream, IEnumerable<T> data = null)
    {
        var config = ExportConfig<T>.GenDefaultConfig(data);
        return await ExportAsync(stream, config, data);
    }
    /// <summary>
    /// 导出excel
    /// </summary>
    public static async Task<Stream> ExportAsync<T>(Stream stream, ExportConfig<T> config, IEnumerable<T> data = null)
    {
        using var read = IExcelCellRead.GetCellRead(stream);
        var exportStream = await ExportAsync(read, config, data);
        read.Save();
        return exportStream;
    }
    /// <summary>
    /// 导出excel
    /// </summary>
    public static async Task<Stream> ExportAsync<T>(string path, ExportConfig<T> config, IEnumerable<T> data = null)
    {
        using var read = IExcelCellRead.GetCellRead(path);
        var exportStream = await ExportAsync(read, config, data);
        read.Save();
        return exportStream;
    }
}