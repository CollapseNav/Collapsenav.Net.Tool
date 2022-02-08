using System.Collections.Concurrent;

namespace Collapsenav.Net.Tool.Excel;

public partial class ReadConfig<T>
{
    /// <summary>
    /// 将表格数据转换为T类型的集合
    /// </summary>
    public async Task<IEnumerable<T>> ToEntityAsync(IExcelRead sheet)
    {
        ConcurrentBag<T> data = new();
        var header = sheet.HeadersWithIndex;
        var rowCount = sheet.RowCount;
        await Task.Factory.StartNew(() =>
        {
            Parallel.For(1, rowCount, index =>
            {
                Monitor.Enter(sheet);
                var dataRow = sheet[index].ToList();
                Monitor.Exit(sheet);
                // 根据对应传入的设置 为obj赋值
                var obj = Activator.CreateInstance<T>();
                foreach (var option in FieldOption)
                {
                    if (!option.ExcelField.IsNull())
                    {
                        var value = dataRow[header[option.ExcelField]];
                        option.Prop.SetValue(obj, option.Action == null ? value : option.Action(value));
                    }
                    else
                        option.Prop.SetValue(obj, option.Action == null ? null : option.Action(string.Empty));
                }
                Init?.Invoke(obj);
                data.Add(obj);
            });
        });
        return data;
    }


    /// <summary>
    /// 转换到实体
    /// </summary>
    public async Task<IEnumerable<T>> ToEntityAsync(ExcelType? excelType = null)
    {
        return await ToEntityAsync(ExcelStream, excelType);
    }
    /// <summary>
    /// 转换到实体
    /// </summary>
    public async Task<IEnumerable<T>> ToEntityAsync(string path, ExcelType? excelType = null)
    {
        using var fs = path.OpenReadShareStream();
        return await ToEntityAsync(fs, excelType);
    }
    /// <summary>
    /// 转换到实体
    /// </summary>
    public virtual async Task<IEnumerable<T>> ToEntityAsync(Stream stream, ExcelType? excelType = null)
    {
        using var reader = IExcelRead.GetExcelRead(stream, excelType);
        return await ToEntityAsync(reader);
    }


    /// <summary>
    /// 转换到实体
    /// </summary>
    public static async Task<IEnumerable<T>> ExcelToEntityAsync(string path)
    {
        return await ExcelTool.ExcelToEntityAsync<T>(path);
    }
    /// <summary>
    /// 转换到实体
    /// </summary>
    public static async Task<IEnumerable<T>> ExcelToEntityAsync(Stream stream)
    {
        return await ExcelTool.ExcelToEntityAsync<T>(stream);
    }
    /// <summary>
    /// 转换到实体
    /// </summary>
    public static async Task<IEnumerable<T>> ExcelToEntityAsync(IExcelRead reader)
    {
        return await ExcelTool.ExcelToEntityAsync<T>(reader);
    }
}
