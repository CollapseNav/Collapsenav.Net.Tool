namespace Collapsenav.Net.Tool.Excel;
/// <summary>
/// 导出表格设置
/// </summary>
public partial class ExportConfig<T>  : ExcelConfig<T, ExportCellOption<T>>
{
    public ExportConfig() { }
    public ExportConfig(IEnumerable<T> data)
    {
        Data = data;
    }
    /// <summary>
    /// 添加普通单元格设置
    /// </summary>
    /// <param name="field">表头列</param>
    /// <param name="propName">属性名称</param>
    public virtual ExportConfig<T> Add(string field, string propName)
    {
        return Add(field, item => item.GetValue(propName));
    }
    /// <summary>
    /// 添加普通单元格设置
    /// </summary>
    /// <param name="check">判断结果</param>
    /// <param name="field">表头列</param>
    /// <param name="propName">属性名称</param>
    public virtual ExportConfig<T> AddIf(bool check, string field, string propName)
    {
        return check ? Add(field, field) : this;
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
