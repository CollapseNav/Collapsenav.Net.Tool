using System.Reflection;

namespace Collapsenav.Net.Tool.Excel;

/// <summary>
/// 表格配置
/// </summary>
public class ExcelConfig<T, CellConfig> where CellConfig : ICellOption<T>, new()
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
    public virtual ExcelConfig<T, CellConfig> Add(CellConfig option)
    {
        FieldOption = FieldOption.Append(option);
        return this;
    }
    /// <summary>
    /// check条件为True时添加普通单元格设置
    /// </summary>
    public virtual ExcelConfig<T, CellConfig> AddIf(bool check, CellConfig option)
    {
        if (check)
            return Add(option);
        return this;
    }
    public virtual ExcelConfig<T, CellConfig> Add(string field, PropertyInfo prop)
    {
        FieldOption = FieldOption.Append(new CellConfig { ExcelField = field, Prop = prop });
        return this;
    }

    public virtual ExcelConfig<T, CellConfig> AddIf(bool check, string field, PropertyInfo prop)
    {
        if (check)
            return Add(field, prop);
        return this;
    }
    /// <smary>
    /// 添加普通单元格设置
    /// </smary>
    /// <param name="field">表头列</param>
    /// <param name="propName">属性名称</param>
    public virtual ExcelConfig<T, CellConfig> Add(string field, string propName)
    {
        FieldOption = FieldOption.Append(new CellConfig { ExcelField = field, PropName = propName });
        return this;
    }
    /// <summary>
    /// 添加普通单元格设置
    /// </summary>
    /// <param name="check">判断结果</param>
    /// <param name="field">表头列</param>
    /// <param name="propName">属性名称</param>
    public virtual ExcelConfig<T, CellConfig> AddIf(bool check, string field, string propName)
    {
        return check ? Add(field, propName) : this;
    }
    /// <summary>
    /// 移除指定单元格配置
    /// </summary>
    public virtual ExcelConfig<T, CellConfig> Remove(string field)
    {
        FieldOption = FieldOption.Where(item => item.ExcelField != field);
        return this;
    }
    public static ExcelConfig<T, CellConfig> GenConfigBySummary()
    {
        var nodes = XmlExt.GetXmlDocuments().GetSummaryNodes().Where(item => item.FullName.Contains(typeof(T).FullName)).ToArray();
        var kvs = typeof(T).BuildInTypePropNames().Select(propName =>
        {
            var node = nodes.FirstOrDefault(item => item.FullName.Split('.').Last() == propName);
            if (node is null)
                return new KeyValuePair<string, string>(propName, propName);
            return new KeyValuePair<string, string>(node.Summary, node.FullName.Split('.').Last());
        }).ToArray();
        var config = new ExcelConfig<T, CellConfig>();
        foreach (var node in kvs)
            config.Add(new CellConfig { ExcelField = node.Key, PropName = node.Value });
        return config;
    }
}

public class DefaultExcelConfig : ExcelConfig<object, BaseCellOption<object>>
{
}