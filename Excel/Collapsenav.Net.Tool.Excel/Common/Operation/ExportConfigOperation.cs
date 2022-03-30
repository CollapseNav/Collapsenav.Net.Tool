namespace Collapsenav.Net.Tool.Excel;

public partial class ExportConfig<T>
{
    /// <summary>
    /// 导出excel
    /// </summary>
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
    /// <summary>
    /// 数据实体导出为Excel
    /// </summary>
    public async Task<Stream> ExportAsync(IEnumerable<T> data = null, ExcelType? excelType = null)
    {
        using var stream = new MemoryStream();
        return await ExportAsync(stream, data, excelType);
    }
    /// <summary>
    /// 数据实体导出为Excel
    /// </summary>
    public async Task<Stream> ExportAsync(string path, IEnumerable<T> data = null, ExcelType? excelType = null)
    {
        using var fs = path.OpenCreateReadWriteShareStream();
        return await ExportAsync(fs, data, excelType);
    }
    /// <summary>
    /// 数据实体导出为Excel
    /// </summary>
    public async Task<Stream> ExportAsync(Stream stream, IEnumerable<T> data = null, ExcelType? excelType = null)
    {
        using IExcelCellRead read = ExcelTool.GetCellRead(stream, excelType);
        var exportStream = await ExportAsync(read, data);
        read.Save();
        return exportStream;
    }
    /// <summary>
    /// 导出表头
    /// </summary>
    public Stream ExportHeader(IExcelCellRead excel)
    {
        foreach (var (value, index) in Header.SelectWithIndex())
            excel[0, index].Value = value;
        return excel.GetStream();
    }
    /// <summary>
    /// 导出表头
    /// </summary>
    public Stream ExportHeader(ExcelType? excelType = null)
    {
        using var stream = new MemoryStream();
        return ExportHeader(stream, excelType);
    }
    /// <summary>
    /// 导出表头
    /// </summary>
    public Stream ExportHeader(string path, ExcelType? excelType = null)
    {
        using var fs = path.OpenCreateReadWriteShareStream();
        return ExportHeader(fs, excelType);
    }
    /// <summary>
    /// 导出表头
    /// </summary>
    public Stream ExportHeader(Stream stream, ExcelType? excelType = null)
    {
        using IExcelCellRead read = ExcelTool.GetCellRead(stream, excelType);
        var exportStream = ExportHeader(read);
        read.Save();
        return exportStream;
    }



    /// <summary>
    /// 数据实体导出为Excel
    /// </summary>
    public static async Task<Stream> DataExportAsync(string path, IEnumerable<T> data)
    {
        var config = GenDefaultConfig(data);
        return await config.ExportAsync(path, data);
    }
    /// <summary>
    /// 数据实体导出为Excel
    /// </summary>
    public static async Task<Stream> DataExportAsync(Stream stream, IEnumerable<T> data)
    {
        var config = GenDefaultConfig(data);
        return await config.ExportAsync(stream);
    }

    /// <summary>
    /// 数据实体导出为Excel
    /// </summary>
    public static Stream ConfigExportHeader(string path)
    {
        var config = GenDefaultConfig();
        return config.ExportHeader(path);
    }
    /// <summary>
    /// 数据实体导出为Excel
    /// </summary>
    public static Stream ConfigExportHeader(Stream stream)
    {
        var config = GenDefaultConfig();
        return config.ExportHeader(stream);
    }
}