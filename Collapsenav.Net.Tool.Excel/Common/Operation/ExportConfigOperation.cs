namespace Collapsenav.Net.Tool.Excel;

public partial class ExportConfig<T>
{
    /// <summary>
    /// 导出excel
    /// </summary>
    /// <param name="excel">excel</param>
    /// <param name="data">指定数据</param>
    public async Task<Stream> ExportAsync(IExcelCellRead excel, IEnumerable<T> data = null)
    {
        await Task.Factory.StartNew(() =>
        {
            foreach (var (value, index) in Header.SelectWithIndex())
                excel[0, index].Value = value;
            foreach (var (cellData, rowIndex) in (data ?? Data).SelectWithIndex(1))
                foreach (var (item, index) in FieldOption.Select((item, i) => (item, i)))
                    excel[rowIndex, index].Value = item.Action(cellData);
        });
        return excel.GetStream();
    }
    public async Task<Stream> ExportAsync(IEnumerable<T> data = null)
    {
        var stream = new MemoryStream();
        return await ExportAsync(stream, data);
    }
    /// <summary>
    /// 数据实体导出为Excel(暂时使用EPPlus实现)
    /// </summary>
    public async Task<Stream> ExportAsync(string path, IEnumerable<T> data = null)
    {
        var fs = path.OpenCreateReadWirteShareStream();
        return await ExportAsync(fs, data);
    }
    /// <summary>
    /// 数据实体导出为Excel(暂时使用EPPlus实现)
    /// </summary>
    public async Task<Stream> ExportAsync(Stream stream, IEnumerable<T> data = null)
    {
        using IExcelCellRead read = IExcelCellRead.GetCellRead(stream);
        var exportStream = await ExportAsync(read, data);
        read.Save();
        return exportStream;
    }

    /// <summary>
    /// 数据实体导出为Excel(暂时使用EPPlus实现)
    /// </summary>
    public static async Task<Stream> DataExportAsync(string path, IEnumerable<T> data)
    {
        var config = GenDefaultConfig(data);
        return await config.ExportAsync(path, data);
    }
    /// <summary>
    /// 数据实体导出为Excel(暂时使用EPPlus实现)
    /// </summary>
    public static async Task<Stream> DataExportAsync(Stream stream, IEnumerable<T> data)
    {
        var config = GenDefaultConfig(data);
        return await EPPlusExportTool.ExportAsync(stream, config);
    }

    /// <summary>
    /// 数据实体导出为Excel(暂时使用EPPlus实现)
    /// </summary>
    public static async Task<Stream> ConfigExportHeaderAsync(string path)
    {
        var config = GenDefaultConfig();
        return await EPPlusExportTool.ExportHeaderAsync(path, config);
    }
    /// <summary>
    /// 数据实体导出为Excel(暂时使用EPPlus实现)
    /// </summary>
    public static async Task<Stream> ConfigExportHeaderAsync(Stream stream)
    {
        var config = GenDefaultConfig();
        return await EPPlusExportTool.ExportHeaderAsync(stream, config);
    }
}