namespace Collapsenav.Net.Tool.Excel;
public partial class ReadConfig<T>
{
    /// <summary>
    /// 获取表格header(仅限简单的单行表头)-Mini
    /// </summary>
    public IEnumerable<string> MiniExcelHeader()
    {
        if (ExcelStream == null)
            throw new Exception();
        return MiniExcelReadTool.ExcelHeader(ExcelStream);
    }
    /// <summary>
    /// 获取表格header(仅限简单的单行表头)-Mini
    /// </summary>x
    public IEnumerable<string> MiniExcelHeader(string filepath)
    {
        return MiniExcelReadTool.ExcelHeader(filepath);
    }
    /// <summary>
    /// 获取表格header(仅限简单的单行表头)-Mini
    /// </summary>
    public IEnumerable<string> MiniExcelHeader(Stream stream)
    {
        return MiniExcelReadTool.ExcelHeader(stream);
    }


    /// <summary>
    /// 根据传入配置 获取表头及其index-Mini
    /// </summary>
    public Dictionary<string, int> MiniExcelHeaderByOptions()
    {
        if (ExcelStream == null)
            throw new Exception();
        return MiniExcelReadTool.ExcelHeaderByOptions(ExcelStream, this);
    }
    /// <summary>
    /// 根据传入配置 获取表头及其index-Mini
    /// </summary>
    public Dictionary<string, int> MiniExcelHeaderByOptions(string filepath)
    {
        return MiniExcelReadTool.ExcelHeaderByOptions(filepath, this);
    }
    /// <summary>
    /// 根据传入配置 获取表头及其index-Mini
    /// </summary>
    public Dictionary<string, int> MiniExcelHeaderByOptions(Stream stream)
    {
        return MiniExcelReadTool.ExcelHeaderByOptions(stream, this);
    }


    /// <summary>
    /// 将表格数据转换为指定的数据实体-Mini
    /// </summary>
    public async Task<IEnumerable<T>> MiniExcelToEntityAsync()
    {
        if (ExcelStream == null)
            throw new Exception();
        return await MiniExcelReadTool.ExcelToEntityAsync(ExcelStream, this);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-Mini
    /// </summary>
    public async Task<IEnumerable<T>> MiniExcelToEntityAsync(string filepath)
    {
        return await MiniExcelReadTool.ExcelToEntityAsync(filepath, this);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-Mini
    /// </summary>
    public async Task<IEnumerable<T>> MiniExcelToEntityAsync(Stream stream)
    {
        return await MiniExcelReadTool.ExcelToEntityAsync(stream, this);
    }
}