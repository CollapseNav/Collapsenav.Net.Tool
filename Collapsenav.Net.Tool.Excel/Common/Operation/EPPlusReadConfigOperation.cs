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
            throw new NullReferenceException();
        return await ToEntityAsync(ExcelStream, ExcelType.EPPlus);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-EPPlus
    /// </summary>
    public async Task<IEnumerable<T>> EPPlusToEntityAsync(string filepath)
    {
        return await ToEntityAsync(filepath, ExcelType.EPPlus);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-EPPlus
    /// </summary>
    public async Task<IEnumerable<T>> EPPlusToEntityAsync(Stream stream)
    {
        return await ToEntityAsync(stream, ExcelType.EPPlus);
    }
    /// <summary>
    /// 将表格数据转换为指定的数据实体-EPPlus
    /// </summary>
    public async Task<IEnumerable<T>> EPPlusToEntityAsync(ExcelWorksheet sheet)
    {
        return await ToEntityAsync(new EPPlusExcelRead(sheet));
    }
}