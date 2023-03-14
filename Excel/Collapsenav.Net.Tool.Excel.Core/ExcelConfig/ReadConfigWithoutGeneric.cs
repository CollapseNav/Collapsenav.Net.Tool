using System.Collections.Concurrent;
using Collapsenav.Net.Tool;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace Collapsenav.Net.Tool.Excel;
/// <summary>
/// 可以不使用泛型的readconfig
/// </summary>
/// <remarks>可以使用泛型定义，但是必须有类型数据</remarks>
public class ReadConfig : ReadConfig<object>
{
    public ReadConfig(Type type, IEnumerable<(string, string, string)> kvs) : base()
    {
        DtoType = type;
        InitFieldOption(kvs);
    }
    public ReadConfig(string typeName, IEnumerable<(string, string, string)> kvs) : this(GetMatchType(typeName), kvs)
    {
    }
    public ReadConfig(Type type, IEnumerable<(string, string)> kvs = null) : base()
    {
        DtoType = type;
        InitFieldOption(kvs);
    }
    public ReadConfig(string typeName, IEnumerable<(string, string)> kvs = null) : base()
    {
        var type = GetMatchType(typeName);
        if (type != null)
            DtoType = type;
        InitFieldOption(kvs);
    }

    public ReadConfig(string typeName, IEnumerable<StringCellOption> options)
    {
        var type = GetMatchType(typeName);
        if (type != null)
            DtoType = type;
        InitFieldOption(options.Select(item => (item.FieldName, item.PropName, item.Func)));
    }
    private static Type GetMatchType(string typeName)
    {
        var hasDot = typeName.Contains('.');
        var types = AppDomain.CurrentDomain.GetCustomerTypes();
        var matchTypes = types.Where(item => item.FullName.Contains(typeName)).ToArray();
        Type matchType = null;
        if (matchTypes?.Length == 1)
        {
            matchType = matchTypes.First();
        }
        else if (matchTypes.Length > 1)
        {
            matchType = matchTypes.FirstOrDefault(item => item.FullName == typeName);
        }
        return matchType;
    }
    /// <summary>
    /// 通过字典初始化配置
    /// </summary>
    /// <param name="kvs">Key为表头名称, Value为属性名称</param>
    public void InitFieldOption(IEnumerable<(string Key, string Value, string Func)> kvs)
    {
        FieldOption = new List<ReadCellOption<object>>();
        if (kvs.NotEmpty())
        {
            foreach (var (Key, Value, Func) in kvs)
                Add(Key, Value, Func);
        }
    }

    /// <summary>
    /// 通过字典初始化配置
    /// </summary>
    /// <param name="kvs">Key为表头名称, Value为属性名称</param>
    public override void InitFieldOption(IEnumerable<(string Key, string Value)> kvs)
    {
        FieldOption = new List<ReadCellOption<object>>();
        if (kvs.NotEmpty())
        {
            foreach (var (Key, Value) in kvs)
                Add(Key, Value);
        }
    }

    /// <summary>
    /// 添加普通单元格设置
    /// </summary>
    /// <param name="field">表头列</param>
    /// <param name="propName">属性名称</param>
    /// <param name="func"></param>
    public ReadConfig Add(string field, string propName, string func)
    {
        if (func.NotEmpty())
        {
            var exp = CSharpScript.EvaluateAsync<Func<string, object>>(func, ScriptOptions.Default.AddReferences(GetType().Assembly)).Result;
            if (exp == null)
                throw new Exception("无法生成表达式, 请检查传入的func字符串");
            base.Add(field, DtoType.GetProperty(propName), exp);
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
    public ReadConfig AddIf(bool check, string field, string propName, string func)
    {
        if (check)
            return Add(field, propName, func);
        return this;
    }
    /// <summary>
    /// 添加普通单元格设置
    /// </summary>
    /// <param name="field">表头列</param>
    /// <param name="propName">属性名称</param>
    public override ReadConfig<object> Add(string field, string propName)
    {
        base.Add(field, DtoType.GetProperty(propName));
        return this;
    }
    /// <summary>
    /// 添加普通单元格设置
    /// </summary>
    /// <param name="check">判断结果</param>
    /// <param name="field">表头列</param>
    /// <param name="propName">属性名称</param>
    public override ReadConfig<object> AddIf(bool check, string field, string propName)
    {
        base.AddIf(check, field, DtoType.GetProperty(propName));
        return this;
    }

    /// <summary>
    /// 将表格数据转换为T类型的集合(更快)
    /// </summary>
    public override IEnumerable<object> ToEntity(IExcelReader sheet)
    {
        var header = sheet.HeadersWithIndex;
        var rowCount = sheet.RowCount;
        List<object> data = new();
        foreach (var index in Enumerable.Range(1, rowCount - 1))
        {
            var dataRow = sheet[index].ToList();
            // 根据对应传入的设置 为obj赋值
            if (dataRow.NotEmpty())
            {
                var obj = Activator.CreateInstance(DtoType);
                foreach (var option in FieldOption)
                {
                    if (option.ExcelField.NotNull())
                    {
                        var value = dataRow[header[option.ExcelField]];
                        option.Prop.SetValue(obj, option.Action == null ? value : option.Action(value));
                    }
                    else
                        option.Prop.SetValue(obj, option.Action == null ? null : option.Action(string.Empty));
                }
                Init?.Invoke(obj);
                data.Add(obj);
                yield return obj;
            }
        }
    }
    /// <summary>
    /// 将表格数据转换为T类型的集合
    /// </summary>
    public override async Task<IEnumerable<object>> ToEntityAsync(IExcelReader sheet)
    {
        var header = sheet.HeadersWithIndex;
        var rowCount = sheet.RowCount;
        if (IsShuffle)
        {
            ConcurrentBag<object> data = new();
            await Task.Factory.StartNew(() =>
            {
                Parallel.For(1, rowCount, index =>
                {
                    Monitor.Enter(sheet);
                    var dataRow = sheet[index].ToList();
                    Monitor.Exit(sheet);
                    // 根据对应传入的设置 为obj赋值
                    if (dataRow.NotEmpty())
                    {
                        var obj = Activator.CreateInstance(DtoType);
                        foreach (var option in FieldOption)
                        {
                            if (option.ExcelField.NotNull())
                            {
                                var value = dataRow[header[option.ExcelField]];
                                option.Prop.SetValue(obj, option.Action == null ? value : option.Action(value));
                            }
                            else
                                option.Prop.SetValue(obj, option.Action == null ? null : option.Action(string.Empty));
                        }
                        Init?.Invoke(obj);
                        data.Add(obj);
                    }
                });
            });
            return data;
        }
        else
        {
            List<object> data = new();
            foreach (var index in Enumerable.Range(1, rowCount - 1))
            {
                var dataRow = sheet[index].ToList();
                // 根据对应传入的设置 为obj赋值
                if (dataRow.NotEmpty())
                {
                    var obj = Activator.CreateInstance<object>();
                    foreach (var option in FieldOption)
                    {
                        if (option.ExcelField.NotNull())
                        {
                            var value = dataRow[header[option.ExcelField]];
                            option.Prop.SetValue(obj, option.Action == null ? value : option.Action(value));
                        }
                        else
                            option.Prop.SetValue(obj, option.Action == null ? null : option.Action(string.Empty));
                    }
                    Init?.Invoke(obj);
                    data.Add(obj);
                }
            }
            return data;
        }
    }

    public static ReadConfig GenConfigBySummary(string typeName)
    {
        var config = new ReadConfig(typeName);
        config.FieldOption = ExcelConfig<object, BaseCellOption<object>>.GenConfigBySummary(config.DtoType).FieldOption.Select(item => new ReadCellOption<object>(item));
        return config;
    }
    public static new ReadConfig GenConfigBySummary(Type type)
    {
        var config = new ReadConfig(type)
        {
            FieldOption = ExcelConfig<object, BaseCellOption<object>>.GenConfigBySummary(type).FieldOption.Select(item => new ReadCellOption<object>(item))
        };
        return config;
    }
}
