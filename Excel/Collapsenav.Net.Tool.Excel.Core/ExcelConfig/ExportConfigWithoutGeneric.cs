using System.Runtime.CompilerServices;
using Collapsenav.Net.Tool;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace Collapsenav.Net.Tool.Excel;
/// <summary>
/// 可以不使用泛型的exportconfig
/// </summary>
/// <remarks>可以不使用泛型定义,但是必须在创建时传data数据</remarks>
public class ExportConfig : ExportConfig<object>
{
    protected ExportConfig() { }
    /// <summary>
    /// 必传data数据确定类型
    /// </summary>
    public ExportConfig(IEnumerable<object> data, IEnumerable<(string, string)> kvs = null) : base(data, kvs) { }
    public ExportConfig(IEnumerable<object> data, IEnumerable<(string, string, string)> kvs) : base(data)
    {
        InitFieldOption(kvs);
    }

    public ExportConfig(IEnumerable<object> data, IEnumerable<StringCellOption> options) : base(data)
    {
        InitFieldOption(options.Select(item => (item.FieldName, item.PropName, item.Func)));
    }
    /// <summary>
    /// 通过字典初始化配置
    /// </summary>
    /// <param name="kvs">Key为表头名称, Value为属性名称</param>
    public void InitFieldOption(IEnumerable<(string Key, string Value, string Func)> kvs)
    {
        FieldOption = new List<ExportCellOption<object>>();
        if (kvs.NotEmpty())
        {
            foreach (var (Key, Value, Func) in kvs)
                Add(Key, Value, Func);
        }
    }

    /// <summary>
    /// 添加普通单元格设置
    /// </summary>
    /// <param name="field">表头列</param>
    /// <param name="propName">属性名称</param>
    /// <param name="func"></param>
    public ExportConfig Add(string field, string propName, string func)
    {
        if (func.NotEmpty())
        {
            var options = ScriptOptions.Default.AddReferences(GetType().Assembly);
            var sc = CSharpScript.Create<object>(func, options, globalsType: DtoType);
            var delega = sc.CreateDelegate();
            Add(field, item => delega(item).Result);
        }
        else
        {
            Add(field, DtoType.GetProperty(propName));
        }
        return this;
    }
    /// <summary>
    /// 添加普通单元格设置
    /// </summary>
    /// <param name="check"></param>
    /// <param name="field">表头列</param>
    /// <param name="propName">属性名称</param>
    /// <param name="func"></param>
    public ExportConfig AddIf(bool check, string field, string propName, string func)
    {
        if (check)
            return Add(field, propName, func);
        return this;
    }
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

    public static ExportConfig InitByExportConfig<T>(ExportConfig<T> config) where T : class
    {
        var exportConfig = new ExportConfig()
        {
            FieldOption = config.FieldOption.Select(item => new ExportCellOption<object>(item.ExcelField, item.Prop, (data) => item.Action((T)data))),
            Data = config.Data,
        };
        // var exportOptions = exportConfig.FieldOption;
        // exportConfig.FieldOption = config.FieldOption.Select(item => new ExportCellOption<object>(item.ExcelField, item.Prop, (data) => item.Action((T)data)));
        return exportConfig;
    }
}