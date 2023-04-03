namespace Collapsenav.Net.Tool.Excel;
public partial class ExportConfig<T>
{
    /// <summary>
    /// 根据 T 生成默认的 Config
    /// </summary>
    public static ExportConfig<T> GenDefaultConfig(IEnumerable<T> data = null)
    {
        var type = data.NotEmpty() ? data.First().GetType() : typeof(T);
        // 根据 T 中设置的 ExcelExportAttribute 创建导入配置
        if (type.AttrValues<ExcelExportAttribute>().NotEmpty())
            return GenConfigByAttribute(data);
        // 直接根据属性名称创建导入配置
        return GenConfigByProps(data);
    }
    /// <summary>
    /// 根据 T 中设置的 ExcelExportAttribute 创建导出配置
    /// </summary>
    [Obsolete("请使用GenConfigByAttribute")]
    public static ExportConfig<T> GenDefaultConfigByAttribute(IEnumerable<T> data = null)
    {
        return GenConfigByAttribute(data);
    }
    /// <summary>
    /// 根据 T 中设置的 ExcelExportAttribute 创建导出配置
    /// </summary>
    public static ExportConfig<T> GenConfigByAttribute(IEnumerable<T> data = null)
    {
        return new ExportConfig<T>(ExcelConfig<T, BaseCellOption<T>>.GenConfigByAttribute<ExcelExportAttribute>()) { Data = data };
    }
    /// <summary>
    /// 直接根据属性名称创建导出配置
    /// </summary>
    [Obsolete("请使用GenConfigByProps")]
    public static ExportConfig<T> GenDefaultConfigByProps(IEnumerable<T> data = null)
    {
        return GenDefaultConfigByProps(data);
    }
    /// <summary>
    /// 直接根据属性名称创建导出配置
    /// </summary>
    public static ExportConfig<T> GenConfigByProps(IEnumerable<T> data = null)
    {
        return new ExportConfig<T>(ExcelConfig<T, BaseCellOption<T>>.GenConfigByProps()) { Data = data };
    }
    public static ExportConfig<T> GenConfigBySummary()
    {
        return new ExportConfig<T>(ExcelConfig<T, BaseCellOption<T>>.GenConfigBySummary());
    }
    /// <summary>
    /// 根据注释生成对应导出配置
    /// </summary>
    public static ExportConfig<T> GenConfigBySummary(IEnumerable<T> data)
    {
        return new ExportConfig<T>(ExcelConfig<T, BaseCellOption<T>>.GenConfigBySummary()) { Data = data };
    }
}