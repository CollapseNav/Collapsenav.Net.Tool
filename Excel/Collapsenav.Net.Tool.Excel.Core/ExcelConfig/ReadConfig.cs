using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using AutoMapper.Internal;

namespace Collapsenav.Net.Tool.Excel;
/// <summary>
/// 表格读取设置
/// </summary>
public partial class ReadConfig<T> : ExcelConfig<T, ReadCellOption<T>>
{
    public bool IsShuffle { get; set; } = true;
    private readonly Stream ExcelStream;
    /// <summary>
    /// 读取成功之后调用的针对T的委托
    /// </summary>
    public Func<T, T> Init { get; protected set; }
    public ReadConfig() : base()
    {
        Init = null;
    }
    /// <summary>
    /// 根据文件路径的初始化
    /// </summary>
    /// <param name="filepath"> 文件路径 </param>
    /// <param name="kvs"></param>
    /// <remarks>从路径中加载文件, 在后续的操作中可以不传</remarks>
    public ReadConfig(string filepath, IEnumerable<(string, string)> kvs = null) : this(kvs)
    {
        ExcelStream = new MemoryStream();
        filepath.IsXls();
        using var fs = filepath.OpenReadShareStream();
        fs.CopyTo(ExcelStream);
    }
    /// <summary>
    /// 根据文件流的初始化
    /// </summary>
    /// <param name="stream">文件流</param>
    /// <param name="kvs"></param>
    /// <remarks>从流中加载, 在后续的操作中可以不传文件</remarks>
    public ReadConfig(Stream stream, IEnumerable<(string, string)> kvs = null) : this(kvs)
    {
        ExcelStream = new MemoryStream();
        stream.CopyTo(ExcelStream);
    }
    /// <summary>
    /// 使用基础的 excelconfig 初始化
    /// </summary>
    public ReadConfig(IExcelConfig<T, BaseCellOption<T>> config) : this()
    {
        FieldOption = config.FieldOption.Select(item => new ReadCellOption<T>(item));
        Data = config.Data;
    }
    public ReadConfig(IEnumerable<(string, string)> kvs) : this()
    {
        InitFieldOption(kvs);
    }

    /// <summary>
    /// 添加默认单元格读取设置(其实就是不读取Excel直接给T的某个字段赋值)
    /// </summary>
    /// <param name="prop">T的属性</param>
    /// <param name="defaultValue">默认值</param>
    public virtual ReadConfig<T> Default<E>(Expression<Func<T, E>> prop, E defaultValue)
    {
        Add(string.Empty, prop, item => defaultValue);
        return this;
    }
    /// <summary>
    /// check条件为True时添加默认单元格读取设置(其实就是不读取Excel直接给T的某个字段赋值)
    /// </summary>
    /// <param name="check">判断结果</param>
    /// <param name="prop">T的属性</param>
    /// <param name="defaultValue">默认值</param>
    public virtual ReadConfig<T> DefaultIf<E>(bool check, Expression<Func<T, E>> prop, E defaultValue)
    {
        return check ? Default(prop, defaultValue) : this;
    }
    /// <summary>
    /// 添加必填的不能为空的读取设置
    /// </summary>
    /// <param name="field">表头列</param>
    /// <param name="prop">T的属性</param>
    /// <param name="action">对单元格字符串的操作</param>
    public virtual ReadConfig<T> Require<E>(string field, Expression<Func<T, E>> prop, Func<string, E> action = null)
    {
        var option = GenOption(field, prop, action);
        var func = option.Action;
        option.Action = item =>
        {
            if (item.IsEmpty())
                throw new NoNullAllowedException($@" {field} 不可为空");
            return func(item);
        };
        Add(option);
        return this;
    }
    /// <summary>
    /// check条件为True时添加必填的不能为空的读取设置
    /// </summary>
    /// <param name="check">判断结果</param>
    /// <param name="field">表头列</param>
    /// <param name="prop">T的属性</param>
    /// <param name="action">对单元格字符串的操作</param>
    public virtual ReadConfig<T> RequireIf<E>(bool check, string field, Expression<Func<T, E>> prop, Func<string, E> action = null)
    {
        return check ? Require(field, prop, action) : this;
    }
    /// <summary>
    /// 添加普通单元格设置
    /// </summary>
    /// <param name="field">表头列</param>
    /// <param name="prop">T的属性</param>
    /// <param name="action">对单元格字符串的操作</param>
    public virtual ReadConfig<T> Add<E>(string field, Expression<Func<T, E>> prop, Func<string, E> action = null)
    {
        Add(GenOption(field, prop, action));
        return this;
    }
    /// <summary>
    /// check条件为True时添加单元格设置
    /// </summary>
    /// <param name="check">判断结果</param>
    /// <param name="field">表头列</param>
    /// <param name="prop">T的属性</param>
    /// <param name="action">对单元格字符串的操作</param>
    public virtual ReadConfig<T> AddIf<E>(bool check, string field, Expression<Func<T, E>> prop, Func<string, E> action = null)
    {
        return check ? Add(field, prop, action) : this;
    }
    /// <summary>
    /// 添加普通单元格设置
    /// </summary>
    /// <param name="field">表头列</param>
    /// <param name="prop">属性</param>
    /// <param name="action">对单元格字符串的操作</param>
    public virtual ReadConfig<T> Add(string field, PropertyInfo prop, Func<string, object> action = null)
    {
        Add(GenOption(field, prop, action));
        return this;
    }
    /// <summary>
    /// 添加普通单元格设置
    /// </summary>
    /// <param name="check">判断结果</param>
    /// <param name="field">表头列</param>
    /// <param name="prop">属性</param>
    /// <param name="action">对单元格字符串的操作</param>
    public virtual ReadConfig<T> AddIf(bool check, string field, PropertyInfo prop, Func<string, object> action = null)
    {
        return check ? Add(field, prop, action) : this;
    }
    /// <summary>
    /// 添加普通单元格设置
    /// </summary>
    /// <param name="field">表头列</param>
    /// <param name="propName">属性名称</param>
    public virtual new ReadConfig<T> Add(string field, string propName)
    {
        base.Add(field, propName);
        return this;
    }
    /// <summary>
    /// 添加普通单元格设置
    /// </summary>
    /// <param name="check">判断结果</param>
    /// <param name="field">表头列</param>
    /// <param name="propName">属性名称</param>
    public virtual new ReadConfig<T> AddIf(bool check, string field, string propName)
    {
        base.AddIf(check, field, propName);
        return this;
    }
    /// <summary>
    /// 添加行数据初始化
    /// </summary>
    /// <param name="action"></param>
    public virtual ReadConfig<T> AddInit(Func<T, T> action)
    {
        Init = action;
        return this;
    }
    /// <summary>
    /// 添加行数据初始化
    /// </summary>
    /// <param name="check">判断结果</param>
    /// <param name="action"></param>
    public virtual ReadConfig<T> AddInitIf(bool check, Func<T, T> action)
    {
        return check ? AddInit(action) : this;
    }
    /// <summary>
    /// 生成单元格设置
    /// </summary>
    /// <param name="field"></param>
    /// <param name="prop"></param>
    /// <param name="action"></param>
    public virtual ReadCellOption<T> GenOption<E>(string field, Expression<Func<T, E>> prop, Func<string, E> action)
    {
        return GenOption(field, (PropertyInfo)prop.GetMember(), action);
    }
    /// <summary>
    /// 生成单元格设置
    /// </summary>
    /// <param name="field"></param>
    /// <param name="prop"></param>
    public virtual ReadCellOption<T> GenOption(string field, PropertyInfo prop)
    {
        return new ReadCellOption<T>(field, prop);
    }
    /// <summary>
    /// 生成单元格设置
    /// </summary>
    /// <param name="field"></param>
    /// <param name="prop"></param>
    /// <param name="action"></param>
    public virtual ReadCellOption<T> GenOption<E>(string field, PropertyInfo prop, Func<string, E> action)
    {
        return action == null ? GenOption(field, prop) : new ReadCellOption<T>(field, prop, item => action(item));
    }
}