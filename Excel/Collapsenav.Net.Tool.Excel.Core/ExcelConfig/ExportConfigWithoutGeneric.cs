using Collapsenav.Net.Tool;

namespace Collapsenav.Net.Tool.Excel;
/// <summary>
/// 可以不使用泛型的exportconfig
/// </summary>
/// <remarks>可以不使用泛型定义,但是必须在创建时传data数据</remarks>
public class ExportConfig : ExportConfig<object>
{
    /// <summary>
    /// 必传data数据确定类型
    /// </summary>
    public ExportConfig(IEnumerable<object> data, IEnumerable<(string, string)> kvs = null) : base(data, kvs) { }

    public static new ExportConfig GenConfigBySummary(IEnumerable<object> data)
    {
        if (data == null)
            throw new NullReferenceException("data 不能为空");
        var config = new ExportConfig(data)
        {
            FieldOption = ExcelConfig<object, BaseCellOption<object>>.GenConfigBySummary(data.First().GetType()).FieldOption.Select(option => new ExportCellOption<object>(option))
        };
        return config;
    }
}