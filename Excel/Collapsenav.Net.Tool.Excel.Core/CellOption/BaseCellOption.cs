using System.Reflection;

namespace Collapsenav.Net.Tool.Excel;
/// <summary>
/// 基础单元格设置配置
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseCellOption<T> : ICellOption
{
    public BaseCellOption()
    {
        // 默认情况下先使用 泛型T 设置 DtoType
        DtoType = typeof(T);
    }

    public BaseCellOption(ICellOption option) : this()
    {
        ExcelField = option.ExcelField;
        PropName = option.PropName;
        Prop = option.Prop;
    }

    /// <summary>
    /// 通过表头和属性名初始化配置
    /// </summary>
    public BaseCellOption(string excelField, string propName) : this()
    {
        ExcelField = excelField;
        PropName = propName;
    }

    /// <summary>
    /// 通过表头和确定的属性初始化配置
    /// </summary>
    public BaseCellOption(string excelField, PropertyInfo prop) : this()
    {
        ExcelField = excelField;
        Prop = prop;
    }
    /// <summary>
    /// 对应excel中的表头字段
    /// </summary>
    public virtual string ExcelField { get; set; }
    /// <summary>
    /// 对应字段的属性(实际上包含PropName)
    /// </summary>
    public virtual PropertyInfo Prop
    {
        get => prop;
        set
        {
            prop = value;
            propName = prop.Name;
            // 当有确切的属性时, 修改DtoType
            DtoType = prop.DeclaringType;
        }
    }
    protected PropertyInfo prop;
    /// <summary>
    /// 就是一个看起来比较方便的标识
    /// </summary>
    public virtual string PropName
    {
        get => propName;
        set
        {
            propName = value;
            prop = DtoType.GetProperty(propName);
        }
    }
    protected string propName;
    /// <summary>
    /// 有时候使用的泛型只是基类, 所以设置一个 DtoType 尝试记录真实类型
    /// </summary>
    public Type DtoType { get; protected set; }
}