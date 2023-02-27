namespace Collapsenav.Net.Tool.Excel;
public partial class ExportConfig<T>
{
    /// <summary>
    /// 根据 T 生成默认的 Config
    /// </summary>
    public static ExportConfig<T> GenDefaultConfig(IEnumerable<T> data = null)
    {
        // 根据 T 中设置的 ExcelExportAttribute 创建导出配置
        if (typeof(T).AttrValues<ExcelExportAttribute>().NotEmpty())
            return GenDefaultConfigByAttribute(data);
        // 直接根据属性名称创建导出配置
        return GenDefaultConfigByProps(data);
    }
    /// <summary>
    /// 根据 T 中设置的 ExcelExportAttribute 创建导出配置
    /// </summary>
    public static ExportConfig<T> GenDefaultConfigByAttribute(IEnumerable<T> data = null)
    {
        var config = new ExportConfig<T>(data);
        var attrData = typeof(T).AttrValues<ExcelExportAttribute>();
        foreach (var prop in attrData)
            config.Add(prop.Value.ExcelField, prop.Key.Name);
        return config;
    }
    /// <summary>
    /// 直接根据属性名称创建导出配置
    /// </summary>
    public static ExportConfig<T> GenDefaultConfigByProps(IEnumerable<T> data = null)
    {
        var config = new ExportConfig<T>(data);
        foreach (var propName in typeof(T).BuildInTypePropNames())
            config.Add(propName, propName);
        return config;
    }
    public static new ExportConfig<T> GenConfigBySummary()
    {
        var config = new ExportConfig<T>(ExcelConfig<T, BaseCellOption<T>>.GenConfigBySummary());
        return config;
    }
    /// <summary>
    /// 根据注释生成对应导出配置
    /// </summary>
    public static ExportConfig<T> GenConfigBySummary(IEnumerable<T> data)
    {
        var config = GenConfigBySummary();
        config.Data = data;
        return config;
    }
}