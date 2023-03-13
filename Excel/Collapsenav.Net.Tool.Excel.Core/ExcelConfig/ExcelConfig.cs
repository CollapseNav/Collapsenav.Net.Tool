using System.Reflection;

namespace Collapsenav.Net.Tool.Excel;

/// <summary>
/// 表格配置
/// </summary>
public class ExcelConfig<T, CellConfig> where CellConfig : ICellOption, new()
{
    public ExcelConfig()
    {
        FieldOption = new List<CellConfig>();
        DtoType = typeof(T);
    }
    public ExcelConfig(IEnumerable<(string, string)> kvs) : this()
    {
        InitFieldOption(kvs);
    }

    public ExcelConfig(IEnumerable<StringCellOption> options) : this()
    {
        FieldOption = options.Select(item => new CellConfig
        {
            ExcelField = item.FieldName,
            PropName = item.PropName,
        });
    }
    protected Type DtoType;
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
    /// 通过元组初始化配置
    /// </summary>
    /// <param name="kvs"></param>
    public virtual void InitFieldOption(IEnumerable<(string Key, string Value)> kvs)
    {
        FieldOption = new List<CellConfig>();
        if (kvs.NotEmpty())
        {
            foreach (var (Key, Value) in kvs)
                Add(Key, Value);
        }
    }
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
    /// <summary>
    /// 添加普通单元格设置
    /// </summary>
    public virtual ExcelConfig<T, CellConfig> Add(string field, PropertyInfo prop)
    {
        FieldOption = FieldOption.Append(new CellConfig { ExcelField = field, Prop = prop });
        return this;
    }
    /// <summary>
    /// 添加普通单元格设置
    /// </summary>
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

    /// <summary>
    /// 直接根据属性名称创建配置
    /// </summary>
    public static ExcelConfig<T, CellConfig> GenConfigByProps()
    {
        var config = new ExcelConfig<T, CellConfig>();
        foreach (var prop in typeof(T).BuildInTypeProps())
            config.Add(prop.Name, prop);
        return config;
    }

    /// <summary>
    /// 根据 T 中设置的 ExcelAttribute 创建配置
    /// </summary>
    public static ExcelConfig<T, CellConfig> GenConfigByAttribute<Attr>() where Attr : ExcelAttribute
    {
        var config = new ExcelConfig<T, CellConfig>();
        var attrData = typeof(T).AttrValues<Attr>();
        foreach (var prop in attrData)
            config.Add(prop.Value.ExcelField, prop.Key);
        return config;
    }

    /// <summary>
    /// 通过注释生成配置
    /// </summary>
    public static ExcelConfig<T, CellConfig> GenConfigBySummary(Type type = null)
    {
        type ??= typeof(T);
        // 获取所有注释node
        var nodes = XmlExt.GetXmlDocuments().GetSummaryNodes().Where(item => item.FullName.Contains(type.FullName)).ToArray();
        var kvs = type.BuildInTypePropNames().Select(propName =>
        {
            var node = nodes.FirstOrDefault(item => item.FullName.Split('.').Last() == propName);
            if (node is null)
                return new KeyValuePair<string, PropertyInfo>(propName, type.GetProperty(propName));
            return new KeyValuePair<string, PropertyInfo>(node.Summary, type.GetProperty(node.FullName.Split('.').Last()));
        }).ToArray();
        var config = new ExcelConfig<T, CellConfig>();
        foreach (var node in kvs)
            config.Add(new CellConfig { ExcelField = node.Key, Prop = node.Value });
        return config;
    }
}
