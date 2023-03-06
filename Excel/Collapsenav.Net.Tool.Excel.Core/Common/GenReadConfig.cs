namespace Collapsenav.Net.Tool.Excel;
public partial class ReadConfig<T>
{
    /// <summary>
    /// 根据 T 生成默认的 Config
    /// </summary>
    public static ReadConfig<T> GenDefaultConfig()
    {
        // 根据 T 中设置的 ExcelExportAttribute 创建导入配置
        if (typeof(T).AttrValues<ExcelReadAttribute>().NotEmpty())
            return GenConfigByAttribute();
        // 直接根据属性名称创建导入配置
        return GenConfigByProps();
    }
    /// <summary>
    /// 根据 T 中设置的 ExcelExportAttribute 创建导入配置
    /// </summary>
    [Obsolete("请使用GenConfigByAttribute")]
    public static ReadConfig<T> GenDefaultConfigByAttribute()
    {
        return new ReadConfig<T>(ExcelConfig<T, BaseCellOption<T>>.GenConfigByAttribute<ExcelReadAttribute>());
    }
    /// <summary>
    /// 根据 T 中设置的 ExcelExportAttribute 创建导入配置
    /// </summary>
    public static ReadConfig<T> GenConfigByAttribute()
    {
        return new ReadConfig<T>(ExcelConfig<T, BaseCellOption<T>>.GenConfigByAttribute<ExcelReadAttribute>());
    }
    /// <summary>
    /// 直接根据属性名称创建导入配置
    /// </summary>
    [Obsolete("请使用GenConfigByProps")]
    public static ReadConfig<T> GenDefaultConfigByProps()
    {
        return new ReadConfig<T>(ExcelConfig<T, BaseCellOption<T>>.GenConfigByProps());
    }
    /// <summary>
    /// 直接根据属性名称创建导入配置
    /// </summary>
    public static new ReadConfig<T> GenConfigByProps()
    {
        return new ReadConfig<T>(ExcelConfig<T, BaseCellOption<T>>.GenConfigByProps());
    }
    /// <summary>
    /// 根据注释生成对应导入配置(data参数无效)
    /// </summary>
    public static ReadConfig<T> GenConfigBySummary()
    {
        var config = new ReadConfig<T>(ExcelConfig<T, BaseCellOption<T>>.GenConfigBySummary());
        return config;
    }
}