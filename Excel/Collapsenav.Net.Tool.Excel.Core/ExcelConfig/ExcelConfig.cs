namespace Collapsenav.Net.Tool.Excel;

/// <summary>
/// 表格配置
/// </summary>
public class ExcelConfig<T, CellConfig> where CellConfig : BaseCellOption<T>
{
    public ExcelConfig()
    {
        FieldOption = new List<CellConfig>();
    }
    /// <summary>
    /// 依据表头的设置
    /// </summary>
    public virtual IEnumerable<CellConfig> FieldOption { get; set; }
    /// <summary>
    /// 表格数据
    /// </summary>
    public IEnumerable<T> Data { get; set; }
    /// <summary>
    /// 获取表头
    /// </summary>
    public virtual IEnumerable<string> Header { get => FieldOption.Select(item => item.ExcelField); }
    /// <summary>
    /// 根据给出的表头筛选options
    /// </summary>
    public void FilterOptionByHeaders(IEnumerable<string> headers)
    {
        FieldOption = FilterOptionByHeaders(FieldOption, headers);
    }
    /// <summary>
    /// 根据给出的表头筛选options
    /// </summary>
    public static IEnumerable<CellConfig> FilterOptionByHeaders(IEnumerable<CellConfig> options, IEnumerable<string> headers)
    {
        return options.Where(item => headers.Where(head => head == item.ExcelField).Any());
    }
    /// <summary>
    /// 添加普通单元格设置
    /// </summary>
    public virtual void Add(CellConfig option)
    {
        FieldOption = FieldOption.Append(option);
    }
    /// <summary>
    /// check条件为True时添加普通单元格设置
    /// </summary>
    public virtual void AddIf(bool check, CellConfig option)
    {
        if (check) Add(option);
    }
    /// <summary>
    /// 移除指定单元格配置
    /// </summary>
    public virtual ExcelConfig<T, CellConfig> Remove(string field)
    {
        FieldOption = FieldOption.Where(item => item.ExcelField != field);
        return this;
    }
}

public class DefaultExcelConfig : ExcelConfig<object, BaseCellOption<object>>
{
}