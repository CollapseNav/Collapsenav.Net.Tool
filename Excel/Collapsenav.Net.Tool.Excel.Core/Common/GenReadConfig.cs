namespace Collapsenav.Net.Tool.Excel;
public partial class ReadConfig<T>
{
    /// <summary>
    /// 根据 T 生成默认的 Config
    /// </summary>
    public static ReadConfig<T> GenDefaultConfig()
    {
        // 根据 T 中设置的 ExcelExportAttribute 创建导入配置
        if (typeof(T).AttrValues<ExcelExportAttribute>().NotEmpty())
            return GenDefaultConfigByAttribute();
        // 直接根据属性名称创建导入配置
        return GenDefaultConfigByProps();
    }
    /// <summary>
    /// 根据 T 中设置的 ExcelExportAttribute 创建导入配置
    /// </summary>
    public static ReadConfig<T> GenDefaultConfigByAttribute()
    {
        var config = new ReadConfig<T>();
        var attrData = typeof(T).AttrValues<ExcelReadAttribute>();
        foreach (var prop in attrData)
            config.Add(prop.Value.ExcelField, prop.Key);
        return config;
    }
    /// <summary>
    /// 直接根据属性名称创建导入配置
    /// </summary>
    public static ReadConfig<T> GenDefaultConfigByProps()
    {
        var config = new ReadConfig<T>();
        // 直接根据属性名称创建导入配置
        foreach (var prop in typeof(T).BuildInTypeProps())
            config.Add(prop.Name, prop);
        return config;
    }
    /// <summary>
    /// 根据注释生成对应导入配置(data参数无效)
    /// </summary>
    public static new ReadConfig<T> GenConfigBySummary()
    {
        var config = new ReadConfig<T>(ExcelConfig<T, BaseCellOption<T>>.GenConfigBySummary());
        return config;
    }
}