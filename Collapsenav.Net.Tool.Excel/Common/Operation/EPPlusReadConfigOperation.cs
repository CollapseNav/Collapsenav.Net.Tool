using OfficeOpenXml;

namespace Collapsenav.Net.Tool.Excel;
public partial class ReadConfig<T>
{
    /// <summary>
    /// 将表格数据转换为指定的数据实体-EPPlus
    /// </summary>
    public async Task<IEnumerable<T>> EPPlusToEntityAsync()
    {
        if (ExcelStream == null)
            throw new Exception();
        return await EPPlusToEntityAsync(ExcelStream);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-EPPlus
    /// </summary>
    public async Task<IEnumerable<T>> EPPlusToEntityAsync(string filepath)
    {
        using var fs = filepath.OpenReadShareStream();
        return await EPPlusToEntityAsync(fs);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-EPPlus
    /// </summary>
    public async Task<IEnumerable<T>> EPPlusToEntityAsync(Stream stream)
    {
        return await EPPlusToEntityAsync(ExcelTool.EPPlusSheet(stream));
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-EPPlus
    /// </summary>
    public async Task<IEnumerable<T>> EPPlusToEntityAsync(ExcelWorksheet sheet)
    {
        return await ToEntityAsync(IExcelRead.GetExcelRead(sheet));
    }
}