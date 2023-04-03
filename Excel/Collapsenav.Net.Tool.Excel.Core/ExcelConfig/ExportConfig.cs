namespace Collapsenav.Net.Tool.Excel;
/// <summary>
/// 导出表格设置
/// </summary>
public partial class ExportConfig<T> : ExcelConfig<T, ExportCellOption<T>>
{
    public ExportConfig() : base() { }
    public ExportConfig(IEnumerable<T> data, IEnumerable<(string, string)> kvs = null) : this()
    {
        Data = data;
        if (Data.NotEmpty())
            DtoType = Data.First().GetType();
        InitFieldOption(kvs);
    }
    /// <summary>
    /// 使用基础的 excelconfig 初始化
    /// </summary>
    public ExportConfig(IExcelConfig<T, BaseCellOption<T>> config)
    {
        FieldOption = config.FieldOption.Select(item => new ExportCellOption<T>(item));
        Data = config.Data;
    }

    /// <summary>
    /// 添加普通单元格设置
    /// </summary>
    /// <param name="field">表头列</param>
    /// <param name="propName">属性名称</param>
    public virtual new ExportConfig<T> Add(string field, string propName)
    {
        base.Add(field, propName);
        return this;
    }
    /// <summary>
    /// 添加普通单元格设置
    /// </summary>
    /// <param name="check">判断结果</param>
    /// <param name="field">表头列</param>
    /// <param name="propName">属性名称</param>
    public virtual new ExportConfig<T> AddIf(bool check, string field, string propName)
    {
        base.AddIf(check, field, propName);
        return this;
    }
    /// <summary>
    /// 添加普通单元格设置
    /// </summary>
    public ExportConfig<T> Add(string field, Func<T, object> action)
    {
        Add(new ExportCellOption<T>
        {
            ExcelField = field,
            Action = action
        });
        return this;
    }
    /// <summary>
    /// 添加普通单元格设置
    /// </summary>
    public ExportConfig<T> AddIf(bool check, string field, Func<T, object> action)
    {
        if (check)
            Add(new ExportCellOption<T>
            {
                ExcelField = field,
                Action = action
            });
        return this;
    }
}
